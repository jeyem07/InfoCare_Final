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

namespace InfoCare_Final
{
    public partial class DoctorDashboard : Form
    {
        private string ServerConnection = "Server=127.0.0.1; Database=db_infocare;User ID=root;Password=";
        private string LoggedInUsername;

        public DoctorDashboard(string username)
        {
            InitializeComponent();
            LoggedInUsername = username;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
            DoctorLogin doctorLogin = new DoctorLogin();
            doctorLogin.Show();
            this.Hide();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Log out succesful");
            DoctorLogin doctorLogin = new DoctorLogin();
            doctorLogin.Show();
            this.Hide();
        }
        private void DoctorDashboard_Load(object sender, EventArgs e)
        {
            LoadDoctorDetails();
        }

        private void LoadDoctorDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(ServerConnection))
            {
                conn.Open();

                string query = "Select d_Firstname, d_lastname from tb_infocare where d_username = @d_username";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@d_username", LoggedInUsername);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["d_Firstname"].ToString();
                            string lastName = reader["d_Lastname"].ToString();


                            DoctorNameLabel.Text = $"{lastName}, {firstName}";
                        }
                        else
                        {
                            DoctorNameLabel.Text = "No data found.";
                        }
                    }
                }
            }


        }
    }
}
