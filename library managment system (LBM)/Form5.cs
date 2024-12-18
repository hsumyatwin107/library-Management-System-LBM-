using library_managment_system__LBM_;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class FPForm : Form
{
    public FPForm()
    {
        InitializeComponent();
    }

    private void buttonSubmit_Click(object sender, EventArgs e)
    {
        string username = textBoxUsername.Text;
        string answer = textBoxAnswer.Text;

        if (string.IsNullOrEmpty(username))
        {
            MessageBox.Show("Please enter your username.");
            return;
        }

        // Retrieve the security question from the database
        string securityQuestion = GetSecurityQuestion(username);

        if (string.IsNullOrEmpty(securityQuestion))
        {
            MessageBox.Show("No user found with this username.");
            return;
        }

        // Display the security question
        labelQuestion.Text = securityQuestion;

        // If the user has entered an answer, validate it
        if (!string.IsNullOrEmpty(answer))
        {
            // Verify if the answer is correct
            if (IsAnswerCorrect(username, answer))
            {
                string password = GetPasswordByUsername(username);
                MessageBox.Show($"Your password is: {password}");

                // Redirect to Login Form
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
            else
            {
                MessageBox.Show("Incorrect answer. Please try again.");
                labelQuestion.Text = "Please answer the security question:";
                textBoxAnswer.Clear();
            }
        }
        else
        {
            MessageBox.Show("Please answer the security question.");
        }
    }

    // Get the security question based on the username
    private string GetSecurityQuestion(string username)
    {
        string connectionString = "Server=MAJEED_17\\SQLEXPRESS;Database=LBM;Integrated Security=True;";
        string query = "SELECT SecurityQuestion FROM Users WHERE Username = @Username";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                object result = command.ExecuteScalar();
                return result?.ToString();
            }
        }
    }

    // Check if the provided answer is correct
    private bool IsAnswerCorrect(string username, string answer)
    {
        string connectionString = "Server=MAJEED_17\\SQLEXPRESS;Database=LBM;Integrated Security=True;";
        string query = "SELECT SecurityAnswer FROM Users WHERE Username = @Username";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                object result = command.ExecuteScalar();
                return result?.ToString().Equals(answer, StringComparison.OrdinalIgnoreCase) ?? false;
            }
        }
    }

    // Get the password by username
    private string GetPasswordByUsername(string username)
    {
        string connectionString = "Server=MAJEED_17\\SQLEXPRESS;Database=LBM;Integrated Security=True;";
        string query = "SELECT Password FROM Users WHERE Username = @Username";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                object result = command.ExecuteScalar();
                return result?.ToString();
            }
        }
    }

    private void FPForm_Load(object sender, EventArgs e)
    {

    }
}
