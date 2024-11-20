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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {

            string username = UsernameTextbox.Text;
            string password = PasswordTextbox.Text;

            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Credentials are empty", "Please try again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (username == "admin" && password == "admin123")
            {
                MessageBox.Show("Login Suucesfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

                AdminDashboardcs adminDashboardcs = new AdminDashboardcs();
                adminDashboardcs.Show();

            }

            else
            {
                MessageBox.Show("Invalid Credentials. Please try again.", "Login Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void ShowpasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowpasswordCheckbox.Checked)
            {
                PasswordTextbox.PasswordChar = '\0';
                PasswordTextbox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordTextbox.PasswordChar = '●';
                PasswordTextbox.UseSystemPasswordChar = true;
            }


        }
        private void HomeButton_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }
    }
}
