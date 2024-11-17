using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InfoCare_Final
{
    public partial class AdminDashboardcs : Form
    {
        private string ServerConnection = "Server=127.0.0.1; Database=db_infocare;User ID=root;Password=";

        private PatientList _patientlist;
        private DoctorList _doctorlist;
        private AllAppointment _allappointment;


        //PatientList Dashboard
        public class PatientList
        {
            private string _connectionstring = "Server=127.0.0.1; Database=db_infocare;User ID=root;Password=";

            public DataTable GetPatients()
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionstring))
                {
                    string query = "SELECT p_firstname as FirstName, p_lastname as LastName, p_username as UserName, p_contact as ContactNumber, p_password as Password from tb_infocare WHERE role = 'Patient'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable patientTable = new DataTable();
                    adapter.Fill(patientTable);
                    return patientTable;
                }
            }
        }

        //DoctorList Dashboard
        public class DoctorList
        {
            private string _connectionstring = "Server=127.0.0.1; Database=db_infocare;User ID=root;Password=";

            public DataTable GetDoctors()
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionstring))
                {
                    string query = "SELECT d_firstname, d_lastname, d_username, d_consultationfee, d_password from tb_infocare WHERE role = 'doctor'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable doctorTable = new DataTable();
                    adapter.Fill(doctorTable);
                    return doctorTable;
                }
            }
        }

        //AllAppointment Dashboard
        public class AllAppointment
        {
            private string _connectionstring = "Server=127.0.0.1; Database=db_infocare;User ID=root;Password=";

            public DataTable GetAllAppointment()
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionstring))
                {
                    string query = "SELECT * from tb_infocare order by role desc";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable AllAppointment = new DataTable();
                    adapter.Fill(AllAppointment);
                    return AllAppointment;
                }
            }
        }



        public AdminDashboardcs()
        {
            InitializeComponent();
            _patientlist = new PatientList();
            _doctorlist = new DoctorList();
            _allappointment = new AllAppointment();

        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Hide();
        }

        private void AddDoctorButton_Click(object sender, EventArgs e)
        {

            Form managedoctor = Application.OpenForms["AdminAddDoctors"];
            if (managedoctor == null)
            {
                AdminAddDoctors adminAddDoctor = new AdminAddDoctors();
                adminAddDoctor.Show();
            }
            else
            {
                managedoctor.BringToFront();
                managedoctor.Focus();
            }
            AddDoctorButtonChangeColor();

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Hide();
        }

        private void PatientListButton_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable patientTable = _patientlist.GetPatients();
                AdminDashboardDatagridview.DataSource = patientTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message} ");
            }
            AdminDashboardDatagridview.Visible = true;
            PatientListButtonChangeColor();

        }

        private void DoctorListButton_Click(object sender, EventArgs e)
        {


            try
            {
                DataTable DoctorTable = _doctorlist.GetDoctors();
                AdminDashboardDatagridview.DataSource = DoctorTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message} ");
            }
            AdminDashboardDatagridview.Visible = true;
            DoctorListButtonChangeColor();
        }

        private void AppointmentHistoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable AllAppointment = _allappointment.GetAllAppointment();
                AdminDashboardDatagridview.DataSource = AllAppointment;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message} ");
            }
            AdminDashboardDatagridview.Visible = true;
            AppointmentListButtonChangeColor();
        }





















        public void PatientListButtonChangeColor()
        {
            PatientListButton.FillColor = Color.FromArgb(60, 128, 174);
            DoctorListButton.FillColor = Color.FromArgb(102, 162, 205);
            AppointmentHistoryButton.FillColor = Color.FromArgb(102, 162, 205);
            AddDoctorButton.FillColor = Color.FromArgb(102, 162, 205);
        }
        public void DoctorListButtonChangeColor()
        {
            DoctorListButton.FillColor = Color.FromArgb(60, 128, 174);
            PatientListButton.FillColor = Color.FromArgb(102, 162, 205);
            AppointmentHistoryButton.FillColor = Color.FromArgb(102, 162, 205);
            AddDoctorButton.FillColor = Color.FromArgb(102, 162, 205);


        }
        public void AppointmentListButtonChangeColor()
        {
            AppointmentHistoryButton.FillColor = Color.FromArgb(60, 128, 174);
            DoctorListButton.FillColor = Color.FromArgb(102, 162, 205);
            PatientListButton.FillColor = Color.FromArgb(102, 162, 205);
            AddDoctorButton.FillColor = Color.FromArgb(102, 162, 205);

        }
        public void AddDoctorButtonChangeColor()
        {
            AddDoctorButton.FillColor = Color.FromArgb(60, 128, 174);
            PatientListButton.FillColor = Color.FromArgb(102, 162, 205);
            DoctorListButton.FillColor = Color.FromArgb(102, 162, 205);
            AppointmentHistoryButton.FillColor = Color.FromArgb(102, 162, 205);
        }
    }

}


