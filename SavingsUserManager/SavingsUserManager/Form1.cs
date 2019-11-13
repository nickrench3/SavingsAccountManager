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
        private SqlConnection con = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS;Initial Catalog=Savings;Integrated Security=True;Pooling=False");
        private SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
            fillCombo();
        }

        void fillCombo()
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

        private void Button1_Click(object sender, EventArgs e)
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
            fillCombo();
        }
    }
}
