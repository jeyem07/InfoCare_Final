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

        private readonly string ServerConnection = "Server=127.0.0.1; Database=db_infocare;User ID=root;Password=";

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

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

            string hashedPassword = HashPassword(PasswordTextbox.Text);

            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();
                string query = "INSERT INTO tb_infocare (p_firstname, p_Lastname, p_username, p_ContactNumber, p_Password, p_hashedpassword) VALUES (@p_firstname, @p_Lastname, @p_username, @p_contactnumber,  @p_Password, @p_HashedPassword)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@p_firstname", FirstnameTextbox.Text);
                    cmd.Parameters.AddWithValue("@p_Lastname", LastnameTextbox.Text);
                    cmd.Parameters.AddWithValue("@p_username", UsernameTextbox.Text);
                    cmd.Parameters.AddWithValue("@p_contactnumber", ContactNumberTextbox.Text);
                    cmd.Parameters.AddWithValue("@p_Password", PasswordTextbox.Text);
                    cmd.Parameters.AddWithValue("@p_HashedPassword", hashedPassword);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration successful!");

                        PatientLogin patientLogin = new PatientLogin();
                        patientLogin.Show();
                        this.Hide();
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

            this.Hide();

        }
    }
}
