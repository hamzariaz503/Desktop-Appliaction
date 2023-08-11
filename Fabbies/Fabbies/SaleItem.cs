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
    public partial class SaleItem : Form
    {
        public SaleItem()
        {
            InitializeComponent();
  
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string productId = textBox1.Text;
            int quantityToSell = int.Parse(textBox2.Text);

            
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-0SQ47JQ\\SQLEXPRESS;Initial Catalog=fabbiesreg;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT quantity FROM inventory1 WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int quantity = reader.GetInt32(0);
                            connection.Close();

                            
                            if (quantity < quantityToSell)
                            {
                                MessageBox.Show("Product quantity is low.");
                            }
                            else
                            {
                                connection.Open();

                               
                                DateTime currentTime = DateTime.Now;

                               
                                string insertQuery = "INSERT INTO sales (productid, quantity, date) VALUES (@ProductId, @Quantity, @Time)";
                                SqlCommand command2 = new SqlCommand(insertQuery, connection);
                                command2.Parameters.AddWithValue("@ProductId", productId);
                                command2.Parameters.AddWithValue("@Quantity", quantityToSell); 
                                command2.Parameters.AddWithValue("@Time", currentTime);

                                command2.ExecuteNonQuery();

                                string updateQuery = "UPDATE inventory1 SET quantity = quantity - @SoldQuantity WHERE id = @ProductId";
                                SqlCommand command3 = new SqlCommand(updateQuery, connection);
                                command3.Parameters.AddWithValue("@SoldQuantity", quantityToSell);
                                command3.Parameters.AddWithValue("@ProductId", productId);

                                command3.ExecuteNonQuery();

                                MessageBox.Show("Successfully sold");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Product not found in the database.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("An error occurred while executing the database query.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Invalid quantity entered. Please enter a valid numeric value.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            dashboard form = new dashboard();
            form.Show();
           
        }

        private void SaleItem_Load(object sender, EventArgs e)
        {
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
