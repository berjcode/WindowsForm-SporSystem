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

            int result = NumberValidation();
         
            if(result >0 )
            {
                MessageBox.Show("Bu telefon numarası sisteme kayıtlı");
            }
            else
            {
                conn.Open();
                string query = "insert into Member (Name,Surname,Number,State,CreatedDate) values (@name,@surname,@number,@state,@date)";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@name", textBox1.Text);
                command.Parameters.AddWithValue("@surname", textBox2.Text);

                command.Parameters.AddWithValue("@number", textBox3.Text);
                command.Parameters.AddWithValue("@state", comboBox1.SelectedItem == null ? "Seçilmedi" : comboBox1.SelectedItem.ToString());
                command.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTime.Now.ToUniversalTime();

                command.ExecuteNonQuery();


                conn.Close();
                MessageBox.Show("Kayıt başarıyla eklendi");


            }
           

        }

        
        public int NumberValidation()
        {
            conn.Open();
            string query2 = "select count(*) from Member where Number=@number  ";
            SqlCommand validationCommand = new SqlCommand(query2, conn);

            validationCommand.Parameters.AddWithValue("@number", textBox3.Text);
            int memberCount = (int)validationCommand.ExecuteScalar();
            conn.Close();

            return memberCount;

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if(dataGridView1.SelectedRows.Count >0 )
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["Name"].Value.ToString();
                textBox2.Text = selectedRow.Cells["Surname"].Value.ToString();  
                textBox3.Text= selectedRow.Cells["Number"].Value.ToString();    

                //textBox4.Text = selectedRow.Cells["Id"].Value.ToString();

               
             
               
               
                foreach (var item in comboBox1.Items)
                {
                    if (item.ToString() == selectedRow.Cells["State"].Value.ToString())
                    {
                        comboBox1.SelectedItem = item;
                        break;
                    }
                }
            }
            
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

          

            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Lütfen Boş Bırakmayın");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            string name = textBox1.Text;
            string surname =textBox2.Text;
            string number = textBox3.Text;
            string state = comboBox1.SelectedItem.ToString();

            int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            string query = "update Member set Name = @name, Surname =@surname , Number = @number ,State =@state where Id = @id";

            SqlCommand command = new SqlCommand(query,conn);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname",surname);
            command.Parameters.AddWithValue("@number", number);

            command.Parameters.AddWithValue("@state", state);

            command.Parameters.AddWithValue("@id", id);

            conn.Open();

            int rowsAffected = command.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show(rowsAffected.ToString() + " kayıt güncellendi.");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();




        }
    }
}
