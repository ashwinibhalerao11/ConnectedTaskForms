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
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ClearForm()
        {
            txtRollNo.Clear();
            txtName.Clear();
            txtBranch.Clear();
            txtPercentage.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "insert into Student values(@name,@branch,@per)";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@branch", txtBranch.Text);
                cmd.Parameters.AddWithValue("@per", Convert.ToDecimal(txtPercentage.Text));
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
                string qry = "select * from Student where rollno=@rollno";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtRollNo.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)//if record is present then return true else false
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtBranch.Text = dr["branch"].ToString();
                        txtPercentage.Text = dr["percentage"].ToString();
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
                string qry = "update Student set name=@name,branch=@branch, percentage=@per where rollno=@rollno";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtRollNo.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@branch", txtBranch.Text);
                cmd.Parameters.AddWithValue("@per", Convert.ToDecimal(txtPercentage.Text));
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
                string qry = "delete from Student where rollno=@rollno";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtRollNo.Text));
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
                string qry = "select * from Student";
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
