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
    public partial class CreateItem : Form
    {
        public CreateItem()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                 string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }


            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-0SQ47JQ\\SQLEXPRESS;Initial Catalog=fabbiesreg;Integrated Security=True");
                con.Open();

                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM inventory1 WHERE id = @id", con);
                checkCmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO inventory1 VALUES (@id, @name, @quantity, @price)", con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@quantity", int.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@price", int.Parse(textBox4.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");
                }
                else
                {
                    MessageBox.Show("Duplicate ID. Cannot insert.");
                }

                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("An error occurred while executing the database query.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Invalid input format. Please enter valid values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboard form2 = new dashboard();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
