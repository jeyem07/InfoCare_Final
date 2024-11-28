using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace InfoCare_Final
{
    public partial class PatientDashboard : Form
    {
        private string ServerConnection = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";
        private Dictionary<string, string> DoctorFees = new Dictionary<string, string>();
        private const string DoctorComboBoxPlaceHolder = "Select a Doctor...";
        private string LoggedInUsername;

        public PatientDashboard(string username)
        {
            InitializeComponent();
            LoggedInUsername = username;
        }

        private void PatientDashboard_Load(object sender, EventArgs e)
        {
            LoadPatientDetails();
            AppointmentDatePicker.MinDate = DateTime.Today;
        }

        private void LoadPatientDetails()
        {
            using (MySqlConnection connection = new MySqlConnection(ServerConnection))
            {
                connection.Open();

                string query = "SELECT Firstname, lastname FROM tb_infocare WHERE username = @username";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", LoggedInUsername);

                    using (MySqlDataReader Datareader = command.ExecuteReader())
                    {
                        if (Datareader.Read())
                        {
                            string firstName = Datareader["Firstname"].ToString();
                            string lastName = Datareader["Lastname"].ToString();

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

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out successful");
            PatientLogin patientlogin = new PatientLogin();
            this.Close();
            patientlogin.Show();
        }

        private void BookAppointmentButton_Click(object sender, EventArgs e)
        {
            BookPanel.Visible = true;
            AppointmentHistoryPanel.Visible = false;

            using (MySqlConnection connection = new MySqlConnection(ServerConnection))
            {
                connection.Open();

                string query = "SELECT lastname, firstname, consultationfee FROM tb_infocare WHERE role = 'doctor'";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader Datareader = command.ExecuteReader();

                DoctorComboBox.Items.Clear();
                DoctorFees.Clear();

                DoctorComboBox.Items.Add(DoctorComboBoxPlaceHolder);
                DoctorComboBox.SelectedIndex = 0;

                while (Datareader.Read())
                {
                    string lastName = Datareader["lastname"].ToString();
                    string firstName = Datareader["firstname"].ToString();
                    string fullName = $"{lastName}, {firstName} ";
                    string consultationFee = Datareader["consultationfee"].ToString();

                    DoctorComboBox.Items.Add(fullName);
                    DoctorFees[fullName] = consultationFee;
                }
                connection.Close();
            }
        }

        private void DoctorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDoctor = DoctorComboBox.SelectedItem?.ToString() ?? "";

            if (selectedDoctor == DoctorComboBoxPlaceHolder)
            {
                ConsultationFeeLabel.Text = "";
                TimeCombobox.Items.Clear();
            }
            else if (DoctorFees.ContainsKey(selectedDoctor))
            {
                ConsultationFeeLabel.Text = DoctorFees[selectedDoctor];

                using (MySqlConnection connection = new MySqlConnection(ServerConnection))
                {
                    connection.Open();
                    string query = @"
                        SELECT DoctorStartTime, DoctorEndTime 
                        FROM tb_infocare 
                        WHERE CONCAT(lastname, ', ', firstname) = @doctorName AND role = 'Doctor'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@doctorName", selectedDoctor);

                        using (MySqlDataReader Datareader = command.ExecuteReader())
                        {
                            if (Datareader.Read())
                            {
                                string startTime1 = Datareader["DoctorStartTime"].ToString();
                                string endTime1 = Datareader["DoctorEndTime"].ToString();

                                if (!string.IsNullOrEmpty(startTime1) && !string.IsNullOrEmpty(endTime1))
                                {
                                    TimeSpan startTime = TimeSpan.Parse(startTime1);
                                    TimeSpan endTime = TimeSpan.Parse(endTime1);

                                    TimeCombobox.Items.Clear();
                                    TimeCombobox.Items.Add("Select a Time Slot");

                                    for (TimeSpan currentTime = startTime; currentTime < endTime; currentTime = currentTime.Add(TimeSpan.FromHours(1)))
                                    {
                                        if (currentTime < endTime)
                                        {
                                            TimeCombobox.Items.Add(currentTime.ToString(@"hh\:mm"));
                                        }
                                    }

                                    TimeCombobox.SelectedIndex = 0;
                                    TimeCombobox.Refresh();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid doctor time range.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No time slots found for this doctor.");
                            }
                        }
                    }
                }
            }
        }



        private void BookSubmitButton_Click(object sender, EventArgs e)
        {
            if (DoctorComboBox.SelectedItem == null || DoctorComboBox.SelectedItem.ToString() == DoctorComboBoxPlaceHolder)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            if (TimeCombobox.SelectedItem == null || TimeCombobox.SelectedItem.ToString() == "Select a Time Slot")
            {
                MessageBox.Show("Please select a time slot.");
                return;
            }

            string patientName = PatientNameLabel.Text;
            string selectedDoctor = DoctorComboBox.SelectedItem.ToString();
            string consultationFee = ConsultationFeeLabel.Text;
            DateTime appointmentDate = AppointmentDatePicker.Value;
            string selectedTime = TimeCombobox.SelectedItem.ToString();

            DateTime appointmentDateTime = DateTime.Parse($"{appointmentDate.ToShortDateString()} {selectedTime}");

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                try
                {
                    conn.Open();

                    string checkerQuery = @"
                SELECT COUNT(*) 
                FROM tb_appointmenthistory
                WHERE DoctorName = @DoctorName 
                AND AppointmentDate = @AppointmentDate 
                AND AppointmentTime = @AppointmentTime;";

                    using (MySqlCommand checkCmd = new MySqlCommand(checkerQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@DoctorName", selectedDoctor);
                        checkCmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                        checkCmd.Parameters.AddWithValue("@AppointmentTime", selectedTime);

                        int appointmentCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (appointmentCount > 0)
                        {
                            MessageBox.Show("This time slot is already booked. Please select a different time.");
                            return;
                        }
                    }

                    string insertQuery = @"
                INSERT INTO tb_appointmenthistory (PatientName, DoctorName, ConsultationFee, AppointmentDate, AppointmentTime) 
                VALUES (@PatientName, @DoctorName, @ConsultationFee, @AppointmentDate, @AppointmentTime);";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@PatientName", patientName);
                        cmd.Parameters.AddWithValue("@DoctorName", selectedDoctor);
                        cmd.Parameters.AddWithValue("@ConsultationFee", consultationFee);
                        cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDateTime);
                        cmd.Parameters.AddWithValue("@AppointmentTime", selectedTime);

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
                    SELECT PatientName AS 'Patient Name', DoctorName AS 'Doctor Name', ConsultationFee AS 'Consultation Fee', AppointmentDate AS 'Appointment Date', AppointmentTime AS 'Appointment Time'
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

        private void PatientNameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
