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
    public partial class ViewItem : Form
    {
        public ViewItem()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewItem_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-0SQ47JQ\\SQLEXPRESS;Initial Catalog=fabbiesreg;Integrated Security=True"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM inventory1", con);
                    

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-0SQ47JQ\\SQLEXPRESS;Initial Catalog=fabbiesreg;Integrated Security=True"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM inventory1 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
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
    }
}
