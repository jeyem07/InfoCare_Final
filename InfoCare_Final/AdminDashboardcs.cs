using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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


        private PatientList _patientlist { get; set; } = new PatientList();
        private DoctorList _doctorlist { get; set; } = new DoctorList();
        private AllAppointment _allappointment { get; set; } = new AllAppointment();


        //PatientList Dashboard
        public class PatientList
        {
            private string _connectionstring = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";

            public DataTable GetPatients()
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionstring))
                {
                    string query = "SELECT firstname as Firstname, lastname as Lastname, username as Username, contactnumber as 'Contact Number', password as Password from tb_infocare WHERE role = 'Patient'";
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
                    DataTable patientTable = new DataTable();
                    dataAdapter.Fill(patientTable);
                    return patientTable;
                }
            }
        }

        //DoctorList Dashboard
        public class DoctorList
        {
            private string _connectionstring = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";

            public DataTable GetDoctors()
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionstring))
                {
                    string query = "SELECT firstname as Firstname, lastname as Lastname, username as Username, consultationfee as 'Consultation Fee', password as Password, Contactnumber as 'Contact Number', DoctorTime as Availability from tb_infocare WHERE role = 'doctor'";
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
                    DataTable doctorTable = new DataTable();
                    dataAdapter.Fill(doctorTable);
                    return doctorTable;
                }
            }
        }

        //AllAppointment Dashboard
        public class AllAppointment
        {
            private string _connectionstring = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";

            public DataTable GetAllAppointment()
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionstring))
                {
                    string query = "select patientname, doctorname, consultationfee, appointmentdate from tb_appointmenthistory;";
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
                    DataTable AllAppointment = new DataTable();
                    dataAdapter.Fill(AllAppointment);
                    return AllAppointment;
                }
            }
        }



        public AdminDashboardcs()
        {
            InitializeComponent();
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Close();

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
            this.Close();
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


