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
        public PatientDashboard()
        {
            InitializeComponent();
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
    }
}
