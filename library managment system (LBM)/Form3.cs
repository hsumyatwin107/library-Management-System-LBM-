using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace library_managment_system__LBM_
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        // Connection string
        private string connectionString = @"Server=MAJEED_17\SQLEXPRESS;Database=LBM;Trusted_Connection=True;";

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string securityQuestion = txtSecurityQuestion.Text.Trim(); // User-entered question
            string securityAnswer = txtSecurityAnswer.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(securityQuestion) || string.IsNullOrEmpty(securityAnswer))
            {
                MessageBox.Show("All fields are required!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (Username, Password, SecurityQuestion, SecurityAnswer) VALUES (@Username, @Password, @SecurityQuestion, @SecurityAnswer)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@SecurityQuestion", securityQuestion);
                        cmd.Parameters.AddWithValue("@SecurityAnswer", securityAnswer);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Create and show the login form without closing the registration form
                            LoginForm loginForm = new LoginForm();
                            loginForm.Show();

                            // Hide the registration form
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
