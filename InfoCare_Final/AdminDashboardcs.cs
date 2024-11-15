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


        public AdminDashboardcs()
        {
            InitializeComponent();
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
            AdminAddDoctors adminAddDoctors = new AdminAddDoctors();
            adminAddDoctors.Show();
            this.Hide();
        }
    }
}
