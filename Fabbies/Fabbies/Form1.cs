using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Fabbies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-0SQ47JQ\\SQLEXPRESS;Initial Catalog=fabbiesreg;Integrated Security=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [dbo].[register]
                            ([firstname]
                            ,[lastname]
                            ,[address]
                            ,[email]
                            ,[phone]
                            ,[username]
                            ,[password])
                            VALUES
                            (@FirstName, @LastName, @Address, @Email, @Phone, @Username, @Password)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {


                        // Validate input
                        if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                            string.IsNullOrWhiteSpace(textBox2.Text) ||
                            string.IsNullOrWhiteSpace(textBox3.Text) ||
                            string.IsNullOrWhiteSpace(textBox6.Text) ||
                            string.IsNullOrWhiteSpace(textBox4.Text) ||
                            string.IsNullOrWhiteSpace(textBox7.Text) ||
                            string.IsNullOrWhiteSpace(textBox5.Text))
                        {
                            MessageBox.Show("Please fill in all the fields.");
                            return;
                        }
                        if (!IsValidEmail(textBox6.Text))
                        {
                            MessageBox.Show("Please enter a valid email address.");
                            return;
                        }
                        if (!IsValidPhoneNumber(textBox4.Text))
                        {
                            MessageBox.Show("Please enter a valid phone number.");
                            return;
                        }
                        
                        cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                        cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Address", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox6.Text);
                        cmd.Parameters.AddWithValue("@Phone", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Username", textBox7.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox5.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registered Successfully.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while executing the SQL query: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            
            string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            return digitsOnly.Length == 11;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
           

        }
    }
}
