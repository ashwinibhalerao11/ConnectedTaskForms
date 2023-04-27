using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace ConnectedTaskForms
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void ClearForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtAuthor.Clear();
            txtPrice.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                //step 1
                string qry = "insert into Book values(@name,@author,@price)";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record inserted");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Book where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)//if record is present then return true else false
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtAuthor.Text = dr["author"].ToString();
                        txtPrice.Text = dr["price"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                    ClearForm();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "update Book set name=@name,author=@author, price=@price where id=@id";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record updated");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "delete from Book where id=@id";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record deleted");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Book";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) // if record is present then return true else false
                {
                    DataTable table = new DataTable();
                    table.Load(dr);// Load() convert object in to table format
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
