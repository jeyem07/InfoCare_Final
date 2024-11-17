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

namespace InfoCare_Final
{
    public partial class PatientDashboard : Form
    {
        private string ServerConnection = "Server=127.0.0.1; Database=db_infocare;User ID=root;Password=";
        private Dictionary<string, string> DoctorFees = new Dictionary<string, string>();
        private const string DoctorComboBoxPlaceHolder = "Select a Doctor...";

        private string LoggedInUsername;

        public PatientDashboard(string username)
        {
            InitializeComponent();
            LoggedInUsername = username;
        }

        private void Logoutlabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out successful");
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out successful");
            PatientLogin patientlogin = new PatientLogin();
            patientlogin.Show();
            this.Hide();
        }

        private void BookAppointmentButton_Click(object sender, EventArgs e)
        {
            BookPanel.Visible = true;
            AppointmentHistoryPanel.Visible = false;

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();

                string query = "SELECT d_lastname, d_firstname, d_consultationfee FROM tb_infocare WHERE role = 'doctor'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader read = cmd.ExecuteReader();

                DoctorComboBox.Items.Clear();
                DoctorFees.Clear();

                DoctorComboBox.Items.Add(DoctorComboBoxPlaceHolder);
                DoctorComboBox.SelectedIndex = 0;

                while (read.Read())
                {
                    string lastName = read["d_lastname"].ToString();
                    string firstName = read["d_firstname"].ToString();
                    string fullName = $"{lastName}, {firstName} ";
                    string consultationFee = read["d_consultationfee"].ToString();

                    DoctorComboBox.Items.Add(fullName);
                    DoctorFees[fullName] = consultationFee;
                }
                conn.Close();
            }
        }

        private void DoctorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDoctor = DoctorComboBox.SelectedItem?.ToString() ?? "";

            if (selectedDoctor == DoctorComboBoxPlaceHolder)
            {
                ConsultationFeeLabel.Text = "";
            }
            else if (DoctorFees.ContainsKey(selectedDoctor))
            {
                ConsultationFeeLabel.Text = DoctorFees[selectedDoctor];
            }
        }

        private void BookSubmitButton_Click(object sender, EventArgs e)
        {
            if (DoctorComboBox.SelectedItem == null || DoctorComboBox.SelectedItem.ToString() == DoctorComboBoxPlaceHolder)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            string patientName = PatientNameLabel.Text; 
            string selectedDoctor = DoctorComboBox.SelectedItem.ToString();
            string consultationFee = ConsultationFeeLabel.Text;
            DateTime appointmentDate = AppointmentDatePicker.Value; 

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                try
                {
                    conn.Open();

                    string insertQuery = @"
                        INSERT INTO tb_appointmenthistory (PatientName, DoctorName, ConsultationFee, AppointmentDate) 
                        VALUES (@PatientName, @DoctorName, @ConsultationFee, @AppointmentDate);";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@PatientName", patientName);
                        cmd.Parameters.AddWithValue("@DoctorName", selectedDoctor);
                        cmd.Parameters.AddWithValue("@ConsultationFee", consultationFee);
                        cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment booked successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to book the appointment.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void PatientDashboard_Load(object sender, EventArgs e)
        {
            LoadPatientDetails();
        }

        private void LoadPatientDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();

                string query = "SELECT p_Firstname, p_lastname FROM tb_infocare WHERE p_username = @p_username";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@p_username", LoggedInUsername);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["p_Firstname"].ToString();
                            string lastName = reader["p_Lastname"].ToString();

                            PatientNameLabel.Text = $"{lastName}, {firstName}";
                        }
                        else
                        {
                            PatientNameLabel.Text = "No data found.";
                        }
                    }
                }
            }
        }

        private void AppointmentHistoryButton_Click(object sender, EventArgs e)
        {
            AppointmentHistoryPanel.Visible = true;
            BookPanel.Visible = false; 

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                try
                {
                    conn.Open();

                    string query = @"
                    SELECT PatientName AS 'Patient Name', DoctorName AS 'Doctor Name', ConsultationFee AS 'Consultation Fee', AppointmentDate AS 'Appointment Date'
                    FROM tb_AppointmentHistory
                    WHERE PatientName = @PatientName;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        string patientName = PatientNameLabel.Text;

                        cmd.Parameters.AddWithValue("@PatientName", patientName);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        AppointmentDatagridview.DataSource = dataTable;
                        AppointmentDatagridview.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading appointment history: " + ex.Message);
                }
            }
        }

    }
}
