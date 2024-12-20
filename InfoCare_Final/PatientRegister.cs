﻿using MySql.Data.MySqlClient;
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

            using (MySqlConnection connection = new MySqlConnection(ServerConnection))
            {
                connection.Open();
                string UserRepQuery = "SELECT COUNT(*) from tb_infocare where username = @username";

                using (MySqlCommand checkCommand = new MySqlCommand(UserRepQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@username", UsernameTextbox.Text);

                    int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (userCount > 0)
                    {
                        MessageBox.Show("The username is already taken. Please choose a different one.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string query = "INSERT INTO tb_infocare (Firstname, Lastname, username, Contactnumber, Password, Role) VALUES (@firstname, @Lastname, @username, @contactnumber,  @Password, @Role)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", FirstnameTextbox.Text);
                        command.Parameters.AddWithValue("@Lastname", LastnameTextbox.Text);
                        command.Parameters.AddWithValue("@username", UsernameTextbox.Text);
                        command.Parameters.AddWithValue("@contactnumber", ContactNumberTextbox.Text);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Role", "Patient");
                        try
                        {
                            command.ExecuteNonQuery();
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
