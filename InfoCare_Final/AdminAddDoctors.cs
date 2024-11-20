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

        public AdminAddDoctors()
        {
            InitializeComponent();
        }

        private void AdminAddDoctors_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            AdminDashboardcs adminDashboardcs = new AdminDashboardcs();
            adminDashboardcs.Show();
            this.Close();

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
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

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();
                string query = "INSERT INTO tb_Infocare (firstname, lastname, username, Consultationfee, Contactnumber, password, Role) VALUES (@firstname, @lastname, @username, @consultationfee, @contactnumber,  @password, @Role)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstname", FirstNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@lastname", LastNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Username", UserNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@consultationfee", ConsultationFeeTextBox.Text);
                    cmd.Parameters.AddWithValue("@contactnumber", Contactnumbertextbox.Text);
                    cmd.Parameters.AddWithValue("@password", Password);
                    cmd.Parameters.AddWithValue("@Role", "Doctor");


                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration successful!");


                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
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
    }
}
