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
        private SqlConnection con = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS01;Initial Catalog=Security;Integrated Security=True;Pooling=False");
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
                    comboBox3.Items.Add(userName);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                
            }
        }

        void savingsCheck(string userName)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT Savings from Login WHERE LoginName = '"+userName+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    bool savings = (bool)dr["Savings"];
                    if (savings == true)
                    {
                        savingsCheckbox.Checked = true;
                    }
                    else
                    {
                        savingsCheckbox.Checked = false;
                    }
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
            comboBox3.Items.Clear();
            fillCombo2();
        }

        private void aprvButton_Click(object sender, EventArgs e)
        {
            string message = comboBox2.Text + " Approved";
            MessageBox.Show(message, "Approve User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string user = comboBox2.Text.Trim();
            if (savingsApproveCheck.Checked == true)
            {
                cmd = new SqlCommand("UPDATE Login SET Added='Y', Savings = 1 WHERE LoginName='" + user + "'", con);
            }
            else
            {
                cmd = new SqlCommand("UPDATE Login SET Added='Y', Savings = 0 WHERE LoginName='" + user + "'", con);
            }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            savingsApproveCheck.Checked = false;
            fillCombo1();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userName = comboBox3.SelectedItem.ToString();
            savingsCheck(userName);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string message = comboBox3.Text + " Updated";
            MessageBox.Show(message, "Update User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string userName = comboBox3.SelectedItem.ToString();
            if (savingsCheckbox.Checked == true)
            {
                cmd = new SqlCommand("UPDATE Login SET Savings = 1 WHERE LoginName='" + userName + "'", con);
            }
            else
            {
                cmd = new SqlCommand("UPDATE Login SET Savings = 0 WHERE LoginName='" + userName + "'", con);
            }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
