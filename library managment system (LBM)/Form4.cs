using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace library_managment_system__LBM_
{
    public partial class BookSelectionForm : Form
    {
        public BookSelectionForm()
        {
            InitializeComponent();
        }

        // Load available books when the form loads
        private void BookSelectionForm_Load(object sender, EventArgs e)
        {
            LoadAvailableBooks();
        }

        // Method to load available books from the database
        private void LoadAvailableBooks()
        {
            // Connection string for SQL Server
            string connectionString = @"Server=MAJEED_17\SQLEXPRESS;Database=LBM;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT BookID, Title, Author, IsBorrowed FROM Books WHERE IsBorrowed = 0"; // Get only available books
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBooks.DataSource = dt;
                }
            }
        }

        // Borrow button click event
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                // Get the selected row (book) from DataGridView
                DataGridViewRow selectedRow = dgvBooks.SelectedRows[0];
                int bookId = Convert.ToInt32(selectedRow.Cells["BookID"].Value);
                string bookTitle = selectedRow.Cells["Title"].Value.ToString();

                // Simulate borrowing the book and update the database
                BorrowBook(bookId);

                MessageBox.Show($"You have borrowed the book: {bookTitle} (ID: {bookId})");

                // Reload the available books after borrowing
                LoadAvailableBooks();
            }
            else
            {
                MessageBox.Show("Please select a book to borrow.");
            }
        }

        // Method to mark a book as borrowed
        private void BorrowBook(int bookId)
        {
            // Connection string for SQL Server
            string connectionString = @"Server=MAJEED_17\SQLEXPRESS;Database=LBM;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Books SET IsBorrowed = 1 WHERE BookID = @bookId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Return button click event
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                // Get the selected row (book) from DataGridView
                DataGridViewRow selectedRow = dgvBooks.SelectedRows[0];
                int bookId = Convert.ToInt32(selectedRow.Cells["BookID"].Value);
                string bookTitle = selectedRow.Cells["Title"].Value.ToString();

                // Simulate returning the book and update the database
                ReturnBook(bookId);

                MessageBox.Show($"You have returned the book: {bookTitle} (ID: {bookId})");

                // Reload the available books after returning
                LoadAvailableBooks();
            }
            else
            {
                MessageBox.Show("Please select a book to return.");
            }
        }

        // Method to mark a book as returned
        private void ReturnBook(int bookId)
        {
            // Connection string for SQL Server
            string connectionString = @"Server=MAJEED_17\SQLEXPRESS;Database=LBM;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Books SET IsBorrowed = 0 WHERE BookID = @bookId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // This method can be used if you need to handle any other events for the DataGridView
        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye! Thank you for using the library system(LBM).", "Goodbye", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Optionally, you can close the current form
            this.Close();
        }

        private void bookSelectionFormBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
