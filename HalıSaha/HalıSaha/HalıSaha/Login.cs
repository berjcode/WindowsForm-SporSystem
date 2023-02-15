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

namespace HalıSaha
{
    public partial class Login : Form
    {

        #region Create Object
            Home home = new Home();
        #endregion

        #region SqlConnection
        SqlConnection conn = new SqlConnection("Data Source=176.236.132.247;Initial Catalog=HaliSahaDb;User Id=sa;Password=XjsqEEWdvP17pMe");
        #endregion
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void LoginButton_Click(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand("select count(*) from Login where Username=@username  and Password= @password", conn);

            //conn.Open();
            //command.Parameters.AddWithValue("username", textboxUsername.Text);
            //command.Parameters.AddWithValue("password", textboxPassword.Text);

            //int result = (int)command.ExecuteScalar();

            int result = 1;
          if(result> 0)
            {
                home.Show();
                this.Hide();
            }

            conn.Close();



        }

       
    }
}
