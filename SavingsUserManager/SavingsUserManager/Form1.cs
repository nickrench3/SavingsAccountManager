using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SavingsUserManager
{
    public partial class Form1 : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS;Initial Catalog=Security;Integrated Security=True;Pooling=False");
        private SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
            fillCombo1();
            fillCombo2();
        }

        void fillCombo1()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT LoginName FROM Login WHERE Added='N'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string userName = (dr["LoginName"].ToString());
                    comboBox2.Items.Add(userName);

                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }
        void fillCombo2()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT LoginName FROM Login", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string userName = (dr["LoginName"].ToString());
                    comboBox1.Items.Add(userName);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void delButton_Click_1(object sender, EventArgs e)
        { 
            string message = comboBox1.Text + " Deleted";
            MessageBox.Show(message, "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string user = comboBox1.Text.Trim();
            con.Open();
            cmd = new SqlCommand("DELETE Login WHERE LoginName='" + user + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            fillCombo2();
        }

        private void aprvButton_Click(object sender, EventArgs e)
        {
            string message = comboBox2.Text + " Approved";
            MessageBox.Show(message, "Approve User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string user = comboBox2.Text.Trim();
            con.Open();
            cmd = new SqlCommand("UPDATE Login SET Added='Y' WHERE LoginName='" + user + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            fillCombo1();
        }
    }
}
