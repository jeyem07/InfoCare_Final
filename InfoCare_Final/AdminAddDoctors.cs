using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace InfoCare_Final
{
    public partial class AdminAddDoctors : Form
    {
        private readonly string ServerConnection = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";
        private const string TimeComboboxPlaceholder = "Select a Time slot.";

        public AdminAddDoctors()
        {
            InitializeComponent();
            ChooseTime();
        }

        private void AdminAddDoctors_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (TimeCombobox.SelectedIndex == 0 || TimeCombobox.SelectedItem.ToString() == TimeComboboxPlaceholder)
            {
                MessageBox.Show("Please select a Time slot.", "Invalid Option", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrEmpty(UserNameTextBox.Text) || String.IsNullOrEmpty(FirstNameTextBox.Text) || String.IsNullOrEmpty(LastNameTextBox.Text) || String.IsNullOrEmpty(ConsultationFeeTextBox.Text) || String.IsNullOrEmpty(PasswordTextBox.Text) || String.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (PasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }

            string Password = PasswordTextBox.Text;

            using (MySqlConnection connection = new MySqlConnection(ServerConnection))
            {
                connection.Open();

                string UserRepQuery = "SELECT COUNT(*) from tb_infocare where username = @username";

                using (MySqlCommand checkCommand = new MySqlCommand(UserRepQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@username", UserNameTextBox.Text);

                    int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (userCount > 0)
                    {
                        MessageBox.Show("The username is already taken. Please choose a different one.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string query = "INSERT INTO tb_Infocare (firstname, lastname, username, Consultationfee, Contactnumber, password, Role, DoctorStartTime, DoctorEndTime, Doctortime) " +
                                   "VALUES (@firstname, @lastname, @username, @consultationfee, @contactnumber, @password, @Role, @DoctorStartTime, @DoctorEndTime, @Doctortime)";

                    using (MySqlCommand commanmd = new MySqlCommand(query, connection))
                    {
                        commanmd.Parameters.AddWithValue("@firstname", FirstNameTextBox.Text);
                        commanmd.Parameters.AddWithValue("@lastname", LastNameTextBox.Text);
                        commanmd.Parameters.AddWithValue("@username", UserNameTextBox.Text);
                        commanmd.Parameters.AddWithValue("@consultationfee", ConsultationFeeTextBox.Text);
                        commanmd.Parameters.AddWithValue("@contactnumber", Contactnumbertextbox.Text);
                        commanmd.Parameters.AddWithValue("@password", Password);
                        commanmd.Parameters.AddWithValue("@Role", "Doctor");


                        string selectedTime = TimeCombobox.SelectedItem?.ToString();
                        if (!string.IsNullOrEmpty(selectedTime))
                        {
                            string[] timeParts = selectedTime.Split('-');
                            if (timeParts.Length == 2)
                            {
                                string doctorStartTime = timeParts[0].Trim();
                                string doctorEndTime = timeParts[1].Trim();

                                commanmd.Parameters.AddWithValue("@DoctorStartTime", doctorStartTime);
                                commanmd.Parameters.AddWithValue("@DoctorEndTime", doctorEndTime);
                                commanmd.Parameters.AddWithValue("@Doctortime", selectedTime);
                            }
                            else
                            {
                                MessageBox.Show("Invalid time range format.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select a valid time range.");
                            return;
                        }

                        try
                        {
                            commanmd.ExecuteNonQuery();
                            MessageBox.Show("Registration successful!");
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void ShowpasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowpasswordCheckbox.Checked)
            {
                PasswordTextBox.PasswordChar = '\0';
                PasswordTextBox.UseSystemPasswordChar = false;

                ConfirmPasswordTextBox.PasswordChar = '\0';
                ConfirmPasswordTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordTextBox.PasswordChar = '●';
                PasswordTextBox.UseSystemPasswordChar = true;

                ConfirmPasswordTextBox.PasswordChar = '●';
                ConfirmPasswordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TimeCombobox.SelectedIndex > 0)
            {
                string selectedtime = TimeCombobox.SelectedItem.ToString();
                MessageBox.Show($"Selected Time: {selectedtime}");
            }
        }

        private void ChooseTime()
        {
            TimeSpan startTime = new TimeSpan(8, 0, 0);
            TimeSpan endTime = new TimeSpan(20, 0, 0);
            TimeSpan DifferenceTime = new TimeSpan(4, 0, 0);

            for (TimeSpan time = startTime; time < endTime; time += DifferenceTime)
            {
                TimeSpan nextTime = time + DifferenceTime;
                string timeString = $"{DateTime.Today.Add(time):HH:mm} - {DateTime.Today.Add(nextTime):HH:mm}";
                TimeCombobox.Items.Add(timeString);

                if (nextTime >= endTime)
                    break;
            }

            TimeCombobox.SelectedIndex = 0;

        }

        private void ConsultationFeeTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
