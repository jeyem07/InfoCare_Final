using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using Guna.UI2.WinForms.Internal;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

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
            MessageBox.Show("Log out succesful");
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
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

                string query = "SELECT d_lastname, d_consultationfee from tb_infocare where role = 'doctor'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader read = cmd.ExecuteReader();

                DoctorComboBox.Items.Clear();
                DoctorFees.Clear();

                DoctorComboBox.Items.Add(DoctorComboBoxPlaceHolder);
                DoctorComboBox.SelectedIndex = 0;

                while (read.Read())
                {
                    string LastName = read["d_lastname"].ToString();
                    string ConsultationFee = read["d_Consultationfee"].ToString();

                    DoctorComboBox.Items.Add(LastName);
                    DoctorFees[LastName] = ConsultationFee;
                }
                conn.Close();

            }
        }

        private void DoctorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedDoctor = "";

            if (DoctorComboBox.SelectedItem != null)
            {
                SelectedDoctor = DoctorComboBox.SelectedItem.ToString();
            }

            if (SelectedDoctor == "Select a Doctor...")
            {
                ConsultationFeeLabel.Text = "";
            }

            else if (DoctorFees.ContainsKey(SelectedDoctor))
            {
                ConsultationFeeLabel.Text = DoctorFees[SelectedDoctor];
            }
        }

        private void BookSubmitButton_Click(object sender, EventArgs e)
        {
            if (DoctorComboBox.SelectedItem == null || DoctorComboBox.SelectedItem.ToString() == DoctorComboBoxPlaceHolder)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            string SelectedDoctor = DoctorComboBox.SelectedItem.ToString();
            string consultationFee = ConsultationFeeLabel.Text;


            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();


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

                string query = "Select p_Firstname, p_lastname from tb_infocare where p_username = @p_username";
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

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                AppointmentDatagridview.Visible = true;

                conn.Open();

                string query = "Select p_Firstname, p_lastname, p_Contact, D_Lastname, D_ConsultationFee from tb_infocare where p_username = @p_username";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@p_username", LoggedInUsername);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    AppointmentDatagridview.DataSource = dataTable;
                }

            }





        }



    }

}