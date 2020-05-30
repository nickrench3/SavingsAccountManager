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

namespace SavingsUserManager
{
    public partial class Login : Form
    {
        private SqlConnection conSecure = new SqlConnection(@"Data Source=NICKRENTSCHLER\SQLEXPRESS;Initial Catalog=Security;Integrated Security=True;Pooling=False");
        private SqlCommand cmd;

        public Login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            conSecure.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT UserID FROM [dbo].[Login] WHERE LoginName='"+userNameTextBox.Text+"' AND PasswordHash=HASHBYTES('SHA2_512', N'" + passwordTextBox.Text + "') AND Added='Y' and Savings = 1", conSecure);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                cmd = new SqlCommand("INSERT LOGINEVENTLOG VALUES('" + userNameTextBox.Text.Trim() + "', '" + DateTime.Now + "', 'Savings User Manager')", conSecure);
                cmd.ExecuteNonQuery();
                conSecure.Close();
                Form1 form1 = new Form1();
                form1.Show();
                this.Owner = form1;
                this.Hide();
            }
            else
            {
                MessageBox.Show("You do not have access to this application!", "Error");
                conSecure.Close();
            }
        }

        private void Form1_FormClosed(object send, FormClosedEventArgs e)
        {
            this.Close();
        }

    }
}
