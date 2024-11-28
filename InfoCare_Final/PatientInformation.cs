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
    public partial class PatientInformation : Form
    {
        private readonly string ServerConnection = "Server=127.0.0.1; Database=db_infocarefinal;User ID=root;Password=";
        public PatientInformation()
        {

            InitializeComponent();
        }

        private void PatientInformationSaveButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FirstNameTextBox.Text) || String.IsNullOrWhiteSpace(LastNameTextBox.Text) || String.IsNullOrWhiteSpace(MiddleNameTextBox.Text) || String.IsNullOrWhiteSpace(ContactNoTextBox.Text) || String.IsNullOrWhiteSpace(GenderComboBox.Text))
            {
                MessageBox.Show("Please fill out all the fields.", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            using (MySqlConnection connection = new MySqlConnection(ServerConnection))
            {
                connection.Open();


                string query = "INSERT INTO tb_patientinformation (Firstname, Lastname, Middlename, Contactnumber, Birthdate, Gender) VALUES (@firstname, @Lastname, @Middlename, @ContactNo, @Gender, @BirthDateTime)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", FirstNameTextBox.Text);
                    command.Parameters.AddWithValue("@Lastname", LastNameTextBox.Text);
                    command.Parameters.AddWithValue("@MiddleName", MiddleNameTextBox.Text);
                    command.Parameters.AddWithValue("@ContactNo", ContactNoTextBox.Text);
                    command.Parameters.AddWithValue("@Gender", GenderComboBox.SelectedItem);
                    command.Parameters.AddWithValue("@BirthDateTime", BirthDateTimePicker.Value);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("successful!");


                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
        }

    }

}


