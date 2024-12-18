using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace library_managment_system__LBM_
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Update the connection string to connect to your SQL Server database
            string connectionString = @"Server=MAJEED_17\SQLEXPRESS;Database=LBM;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Open the database connection

                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        int count = Convert.ToInt32(cmd.ExecuteScalar()); // Execute the query

                        if (count == 1)
                        {
                            // If login is successful, navigate to the book selection form
                            BookSelectionForm bookForm = new BookSelectionForm();
                            bookForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Label click event (if needed)
        }
       

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to the WelcomeForm
            WelcomeForm previous = new WelcomeForm();
            previous.Show();
            this.Hide();
        }

        private void buttonForgotPassword_Click_1(object sender, EventArgs e)
        {
            FPForm fpForm = new FPForm();
            fpForm.Show();
            this.Hide();
        }
    }
}
