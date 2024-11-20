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
            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();

                string query = "Select Firstname, lastname from tb_infocare where username = @username";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", LoggedInUsername);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["Firstname"].ToString();
                            string lastName = reader["Lastname"].ToString();


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

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                ViewAppointmentsPanel.Visible = true;
                try
                {
                    conn.Open();

                    string query = @" SELECT PatientName AS 'Patient Name', DoctorName AS 'Doctor Name', ConsultationFee AS 'Consultation Fee', AppointmentDate AS 'Appointment Date'
                                      FROM tb_AppointmentHistory
                                      WHERE DoctorName = @DoctorName";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        string doctorName = DoctorNameLabel.Text;

                        cmd.Parameters.AddWithValue("@DoctorName", doctorName);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

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
    }
}

