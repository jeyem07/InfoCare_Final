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
    public partial class DoctorDashboard : Form
    {
        private string ServerConnection = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";
        private string LoggedInUsername;

        public DoctorDashboard(string username)
        {
            InitializeComponent();
            LoggedInUsername = username;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
            DoctorLogin doctorLogin = new DoctorLogin();
            this.Close();
            doctorLogin.Show();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
            DoctorLogin doctorLogin = new DoctorLogin();
            doctorLogin.Show();
            this.Close();
        }
        private void DoctorDashboard_Load(object sender, EventArgs e)
        {
            LoadDoctorDetails();
        }

        private void LoadDoctorDetails()
        {
            using (MySqlConnection connection = new MySqlConnection(ServerConnection))
            {
                connection.Open();

                string query = "Select Firstname, lastname from tb_infocare where username = @username";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", LoggedInUsername);

                    using (MySqlDataReader Datareader = command.ExecuteReader())
                    {
                        if (Datareader.Read())
                        {
                            string firstName = Datareader["Firstname"].ToString();
                            string lastName = Datareader["Lastname"].ToString();


                            DoctorNameLabel.Text = $"{lastName}, {firstName}";
                        }
                        else
                        {
                            DoctorNameLabel.Text = "No data found.";
                        }
                    }
                }
            }


        }

        private void ViewAppointmentsButton_Click(object sender, EventArgs e)
        {

            using (MySqlConnection connection = new MySqlConnection(ServerConnection))
            {
                ViewAppointmentsPanel.Visible = true;
                try
                {
                    connection.Open();

                    string query = @" SELECT PatientName AS 'Patient Name', DoctorName AS 'Doctor Name', ConsultationFee AS 'Consultation Fee', AppointmentDate AS 'Appointment Date', AppointmentTime AS 'Appointment Time'
                                      FROM tb_AppointmentHistory
                                      WHERE DoctorName = @DoctorName";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        string doctorName = DoctorNameLabel.Text;

                        command.Parameters.AddWithValue("@DoctorName", doctorName);

                        MySqlDataAdapter Dataadapter = new MySqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        Dataadapter.Fill(dataTable);

                        ViewAppointmentsDatagrid.DataSource = dataTable;
                        ViewAppointmentsDatagrid.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading appointment history: " + ex.Message);
                }
            }
        }

        private void PatientinformationButton_Click(object sender, EventArgs e)
        {
            Form patientInformation = Application.OpenForms["Patientinformation"];
            if (patientInformation == null)
            {
                PatientInformation patientinformation = new PatientInformation();
                patientinformation.Show();
            }
            else
            {
                patientInformation.BringToFront();
                patientInformation.Focus();
            }
        }
    }
}

