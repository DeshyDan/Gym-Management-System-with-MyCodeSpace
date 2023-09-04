using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gym_Management_System_with_MyCodeSpace.Properties;

namespace Gym_Management_System_with_MyCodeSpace
{
    public partial class AddMembers : Form
    {
        #region instance variables
        private string connectionString = Settings.Default.GymDBConnectionString;

        private SqlConnection connection;

        #endregion

        #region Constructor
        public AddMembers()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
        }
        #endregion
        private void AddMembers_Load(object sender, EventArgs e)
        {

        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            if (nameInput.Text == "" || phoneInput.Text == "" || amountInput.Text == "" || ageInput.Text == "" || genderPicker.SelectedItem == null || timingPicker.SelectedItem == null)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    connection.Open();
                    string query = "insert into Members(Name, Phone, Amount, Age, Gender, Timing) values(@Name, @Phone, @Amount, @Age, @Gender, @Timing)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Name", nameInput.Text);
                    command.Parameters.AddWithValue("@Phone", phoneInput.Text);
                    command.Parameters.AddWithValue("@Amount", amountInput.Text);
                    command.Parameters.AddWithValue("@Age", ageInput.Text);
                    command.Parameters.AddWithValue("@Gender", genderPicker.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Timing", timingPicker.SelectedItem.ToString());

                  
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 1)
                        MessageBox.Show("Member has successfully been added");
                    else
                        MessageBox.Show("No rows were affected");

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
