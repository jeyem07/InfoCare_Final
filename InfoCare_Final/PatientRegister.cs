using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InfoCare_Final.AdminDashboardcs;
using BCrypt.Net;



namespace InfoCare_Final
{
    public partial class PatientRegister : Form
    {
        public PatientRegister()
        {
            InitializeComponent();
        }

        private readonly string ServerConnection = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";


        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FirstnameTextbox.Text) || String.IsNullOrWhiteSpace(LastnameTextbox.Text) || String.IsNullOrWhiteSpace(UsernameTextbox.Text) || String.IsNullOrWhiteSpace(ContactNumberTextbox.Text) || String.IsNullOrWhiteSpace(PasswordTextbox.Text) || String.IsNullOrWhiteSpace(ConfirmPasswordTextbox.Text))
            {
                MessageBox.Show("Please fill out all the fields.", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (PasswordTextbox.Text != ConfirmPasswordTextbox.Text)
            {
                MessageBox.Show("Password do not match", " Please try again.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string Password = PasswordTextbox.Text;

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();
                string query = "INSERT INTO tb_infocare (Firstname, Lastname, username, Contactnumber, Password, Role) VALUES (@firstname, @Lastname, @username, @contactnumber,  @Password, @Role)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstname", FirstnameTextbox.Text);
                    cmd.Parameters.AddWithValue("@Lastname", LastnameTextbox.Text);
                    cmd.Parameters.AddWithValue("@username", UsernameTextbox.Text);
                    cmd.Parameters.AddWithValue("@contactnumber", ContactNumberTextbox.Text);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Role", "Patient");
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration successful!");

                        PatientLogin patientLogin = new PatientLogin();
                        patientLogin.Show();
                        this.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }


        }

        private void LoginLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatientLogin patientLogin = new PatientLogin();
            patientLogin.Show();

            this.Close();

        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void ShowpasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowpasswordCheckbox.Checked)
            {
                PasswordTextbox.PasswordChar = '\0';
                PasswordTextbox.UseSystemPasswordChar = false;
                ConfirmPasswordTextbox.PasswordChar = '\0';
                ConfirmPasswordTextbox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordTextbox.PasswordChar = '●';
                PasswordTextbox.UseSystemPasswordChar = true;
                ConfirmPasswordTextbox.PasswordChar = '●';
                ConfirmPasswordTextbox.UseSystemPasswordChar = true;
            }
        }
    }
}
