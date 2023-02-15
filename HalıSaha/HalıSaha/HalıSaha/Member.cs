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

namespace HalıSaha
{
    public partial class Member : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=176.236.132.247;Initial Catalog=HaliSahaDb;User Id=sa;Password=XjsqEEWdvP17pMe");
        public Member()
        {
            InitializeComponent();
        }

        private void Member_Load(object sender, EventArgs e)
        {
            CombolistUpload();


            //settings

            textBox3.MaxLength = 12;
        }




        private void CombolistUpload()
        {
            comboBox1.Items.Add("Normal");
            comboBox1.Items.Add("Vip");
            comboBox1.Items.Add("Yasaklı");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen boş yer bırakmayın", "Uyarı");
                return;
            }

            conn.Open();
            string query = "insert into Member (Name,Surname,Number,State,CreatedDate) values (@name,@surname,@number,@state,@date)";
            
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.Parameters.AddWithValue("@surname", textBox2.Text);

            command.Parameters.AddWithValue("@number",textBox3.Text);
            command.Parameters.AddWithValue("@state", comboBox1.SelectedItem == null ? "Seçilmedi" : comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTime.Now.ToUniversalTime();

            command.ExecuteNonQuery();


            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {


            DataTable dataTable = new DataTable();


            string query = "select * from Member";

            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

            adapter.Fill(dataTable);


            conn.Close();

            dataGridView1.DataSource = dataTable;


        }

     
    }
}
