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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Fabbies
{
    public partial class DeleteItem : Form
    {
        public DeleteItem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
         
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-0SQ47JQ\\SQLEXPRESS;Initial Catalog=fabbiesreg;Integrated Security=True"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM inventory1 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item successfully deleted.");
                    }
                    else
                    {
                        MessageBox.Show("No item found for the given ID.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while executing the SQL query: " + ex.Message);
            }
            catch (Exception ex)
            {
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
        }
    }
}
