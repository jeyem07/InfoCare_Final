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
using BCrypt.Net;

namespace InfoCare_Final
{
    public partial class PatientLogin : Form
    {
        private readonly string ServerConnection = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";
       

        public PatientLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextbox.Text;
            string password = PasswordTextbox.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill out all fields.", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();
                string query = "SELECT Password,Role FROM tb_infocare WHERE username = @username and role = 'patient'";
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            string storedPassword = reader["Password"].ToString();
                            
                                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                
                                PatientDashboard patientDashboard = new PatientDashboard(username);
                                patientDashboard.Show();

                                this.Close();
                                return;
                            
                        }

                        else
                        {
                            MessageBox.Show("Invalid Credentials!", "Try Again.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }

        private void RegisterLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatientRegister patientRegister = new PatientRegister();
            patientRegister.Show();
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

            }
            else
            {
                PasswordTextbox.PasswordChar = '●';
                PasswordTextbox.UseSystemPasswordChar = true;
            }
        }
    }
}
