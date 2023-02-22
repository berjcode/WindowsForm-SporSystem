using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace HalıSaha
{
    public partial class Home : Form
    {

       

        #region SqlConnection
        SqlConnection conn = new SqlConnection("Data Source=xx;Initial Catalog=HaliSahaDb;");
        #endregion

        //background





        public int sayac = 0;


        public Home()
        {
            InitializeComponent();
            Getlist();



        }
        private void Home_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.ForestGreen;
            this.panel9.Visible = false;
            Getlist();

        }

        private void Button_Click(object sender, EventArgs e)
        {


         
          
           





            //MessageBox.Show("Bilgiler: " + button.Text, "Detay");




        }



        //randevu silme
       

       

        private void button1_Click(object sender, EventArgs e)
        {

            Register register = new Register();
            register.Show();
          

           
           
        }


       public void Getlist()
        {

           

            GetListAppoitnmentMonday();
            GetListAppoitnmentThuesday(panel2);
            GetListAppoitnmentWednesday(panel3);
            GetListAppoitnmentThursday(panel4);
            GetListAppoitnmentFriday(panel5);
            GetListAppoitnmentSaturday(panel6);
            GetListAppoitnmentSunday(panel7);

          
        }
        //public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //    // DataGridView kontrolüne erişim
        //    DataGridView dgv = dataGridView1;

        //    // Her satırda döngü
        //    foreach (DataGridViewRow row in dgv.Rows)
        //    {

        //        if (dataGridView1.Columns.Count >= 4 && row.Cells.Count >= 4)
        //        {
        //            DataGridViewCell cell = row.Cells[6];
        //            if (cell.Value != null && cell.Value.ToString() == "Rezerve")
        //            {
        //                cell.Style.BackColor = Color.Green;
        //            }


        //            if (cell.Value != null && cell.Value.ToString() == "Yasaklı")
        //                cell.Style.BackColor = Color.Red;

        //        }
        //    }

        //}

        private void Yenile_Click(object sender, EventArgs e)
        {
            Getlist();

            MessageBox.Show("Veriler Yenilendi","Yenile Mesajı");
        

        }


        #region Monday
      public void GetListAppoitnmentMonday()
        {
           
            DataTable myDataTable = new DataTable();
            DateTime today = DateTime.Today;
         //   string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Pazartesi' order by RandevuSaati asc";
            string query = "SELECT * FROM Randevutbl WHERE RandevuGunu= 'Pazartesi' and RandevuTarihi >= @today   ORDER BY RandevuSaati ASC,RandevuTarihi asc";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@today", today);
               conn.Open();
             
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(myDataTable);

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;

            // DataTable nesnesindeki her satır için bir buton oluşturuyoruz.
            int buttonWidth = 150;
            int buttonHeight = 50;
            int y = 10;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                DataRow row = myDataTable.Rows[i];

                // Buton nesnesini oluşturuyoruz.
                DateTime date = (DateTime)row["RandevuTarihi"];
                int id = (int)row["Id"];
                Button button = new Button();
                button.Text = "Rezerve Tarihi:"+date.ToString("dd.MM.yyyy") + "     "+ "Rezerve Saati" + row["RandevuSaati"] +"   "+ " Rezerve eden: " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " Telefon Numarası: " + row["Telefon"].ToString() + " Saha Adı: " + row["Saha"].ToString();

                button.Width = buttonWidth;
                button.Height = buttonHeight;

               
                button.Tag = id;
             
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                //button.Click += new EventHandler(Button_Click);
                button.Click += new EventHandler(Button_Delete);
                





                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.ForeColor = Color.Beige;
                    button.BackColor = Color.Green;
                    //button.FlatAppearance.BorderColor = Color.Green;
                    button.FlatAppearance.BorderSize = 2;


                   

                }
                
                if (row["Durum"].ToString() == "Beklemede")
                {

                   
                    button.ForeColor = Color.Red;
                }
                // Butonu panele ekliyoruz.
                panel1.Controls.Add(button);
                y += 60;

               
            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            this.Controls.Add(panel);
        }

      
        #endregion



        #region thuesday

       public void GetListAppoitnmentThuesday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl WHERE RandevuGunu= 'Salı' and RandevuTarihi >= @today   ORDER BY RandevuSaati ASC,RandevuTarihi asc";
            DateTime today = DateTime.Today;
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@today", today);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(myDataTable);

            //Panel panel = new Panel();
            //  panel2.Dock = DockStyle.Fill;

            // DataTable nesnesindeki her satır için bir buton oluşturuyoruz.
            int buttonWidth = 150;
            int buttonHeight = 50;
            int y = 10;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                DataRow row = myDataTable.Rows[i];

                // Buton nesnesini oluşturuyoruz.
                DateTime date = (DateTime)row["RandevuTarihi"];
                int id = (int)row["Id"];
                Button button = new Button();
                button.Text = "Rezerve Tarihi:" + date.ToString("dd.MM.yyyy") + "     " + "Rezerve Saati" + row["RandevuSaati"] + "   " + " Rezerve eden: " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " Telefon Numarası: " + row["Telefon"].ToString() + " Saha Adı: " + row["Saha"].ToString();
                button.Width = buttonWidth;
                button.Tag = id;
                button.Height = buttonHeight;
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                //bu/*tton.Click += new EventHandler(Button_Click);*/
                button.Click += new EventHandler(Button_Delete);
                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.ForeColor = Color.Beige;
                    button.BackColor = Color.Green;
                    //button.FlatAppearance.BorderColor = Color.Green;
                    button.FlatAppearance.BorderSize = 2;




                }

                if (row["Durum"].ToString() == "Beklemede")
                {


                    button.ForeColor = Color.Red;
                }
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

       

        #endregion



        #region Wednesday

      public    void GetListAppoitnmentWednesday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl WHERE RandevuGunu= 'Çarşamba' and RandevuTarihi >= @today   ORDER BY RandevuSaati ASC,RandevuTarihi asc";
            DateTime today = DateTime.Today;
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@today", today);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(myDataTable);

            //Panel panel = new Panel();
            //  panel2.Dock = DockStyle.Fill;

            // DataTable nesnesindeki her satır için bir buton oluşturuyoruz.
            int buttonWidth = 150;
            int buttonHeight = 50;
            int y = 10;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                DataRow row = myDataTable.Rows[i];

                // Buton nesnesini oluşturuyoruz.

                DateTime date = (DateTime)row["RandevuTarihi"];
                int id = (int)row["Id"];
                Button button = new Button();
                button.Text = "Rezerve Tarihi:" + date.ToString("dd.MM.yyyy") + "     " + "Rezerve Saati" + row["RandevuSaati"] + "   " + " Rezerve eden: " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " Telefon Numarası: " + row["Telefon"].ToString() + " Saha Adı: " + row["Saha"].ToString();
                button.Tag = id;
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                //button.Click += new EventHandler(Button_Click);
                //button.Click += new EventHandler(button3_Click);
                button.Click += new EventHandler(Button_Delete);

                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.ForeColor = Color.Beige;
                    button.BackColor = Color.Green;
                    //button.FlatAppearance.BorderColor = Color.Green;
                    button.FlatAppearance.BorderSize = 2;




                }

                if (row["Durum"].ToString() == "Beklemede")
                {


                    button.ForeColor = Color.Red;
                }
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

      

        #endregion


        #region Thurday 

       public void GetListAppoitnmentThursday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl WHERE RandevuGunu= 'Perşembe' and RandevuTarihi >= @today   ORDER BY RandevuSaati ASC,RandevuTarihi asc";
            DateTime today = DateTime.Today;
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@today", today);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(myDataTable);

            //Panel panel = new Panel();
            //  panel2.Dock = DockStyle.Fill;

            // DataTable nesnesindeki her satır için bir buton oluşturuyoruz.
            int buttonWidth = 150;
            int buttonHeight = 50;
            int y = 10;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                DataRow row = myDataTable.Rows[i];

                // Buton nesnesini oluşturuyoruz.

                DateTime date = (DateTime)row["RandevuTarihi"];
                int id = (int)row["Id"];
                Button button = new Button();
                button.Text = "Rezerve Tarihi:" + date.ToString("dd.MM.yyyy") + "     " + "Rezerve Saati" + row["RandevuSaati"] + "   " + " Rezerve eden: " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " Telefon Numarası: " + row["Telefon"].ToString() + " Saha Adı: " + row["Saha"].ToString();
                button.Tag = id;
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                //button.Click += new EventHandler(Button_Click);
                button.Click += new EventHandler(Button_Delete);
                //button.Click += new EventHandler(button3_Click);



                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.ForeColor = Color.Beige;
                    button.BackColor = Color.Green;
                    //button.FlatAppearance.BorderColor = Color.Green;
                    button.FlatAppearance.BorderSize = 2;




                }

                if (row["Durum"].ToString() == "Beklemede")
                {


                    button.ForeColor = Color.Red;
                }
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }



        #endregion


        #region Friday

        public void GetListAppoitnmentFriday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl WHERE RandevuGunu= 'Cuma' and RandevuTarihi >= @today   ORDER BY RandevuSaati ASC,RandevuTarihi asc";
            DateTime today = DateTime.Today;
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@today", today);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(myDataTable);

            //Panel panel = new Panel();
            //  panel2.Dock = DockStyle.Fill;

            // DataTable nesnesindeki her satır için bir buton oluşturuyoruz.
            int buttonWidth = 150;
            int buttonHeight = 50;
            int y = 10;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                DataRow row = myDataTable.Rows[i];

                // Buton nesnesini oluşturuyoruz.

                DateTime date = (DateTime)row["RandevuTarihi"];
                int id = (int)row["Id"];
                Button button = new Button();
                button.Text = "Rezerve Tarihi:" + date.ToString("dd.MM.yyyy") + "     " + "Rezerve Saati" + row["RandevuSaati"] + "   " + " Rezerve eden: " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " Telefon Numarası: " + row["Telefon"].ToString() + " Saha Adı: " + row["Saha"].ToString();
                button.Width = buttonWidth;
                button.Tag = id;
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                //button.Click += new EventHandler(Button_Click);
                button.Click += new EventHandler(Button_Delete);

                //button.Click += new EventHandler(button3_Click);


                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.ForeColor = Color.Beige;
                    button.BackColor = Color.Green;
                    //button.FlatAppearance.BorderColor = Color.Green;
                    button.FlatAppearance.BorderSize = 2;




                }

                if (row["Durum"].ToString() == "Beklemede")
                {


                    button.ForeColor = Color.Red;
                }
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }



        #endregion


        #region Saturday

        public void GetListAppoitnmentSaturday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl WHERE RandevuGunu= 'Cumartesi' and RandevuTarihi >= @today   ORDER BY RandevuSaati ASC,RandevuTarihi asc";
            DateTime today = DateTime.Today;
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@today", today);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(myDataTable);

            //Panel panel = new Panel();
            //  panel2.Dock = DockStyle.Fill;

            // DataTable nesnesindeki her satır için bir buton oluşturuyoruz.
            int buttonWidth = 150;
            int buttonHeight = 50;
            int y = 10;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                DataRow row = myDataTable.Rows[i];

                // Buton nesnesini oluşturuyoruz.


                DateTime date = (DateTime)row["RandevuTarihi"];
                int id = (int)row["Id"];
                Button button = new Button();
                button.Text = "Rezerve Tarihi:" + date.ToString("dd.MM.yyyy") + "     " + "Rezerve Saati" + row["RandevuSaati"] + "   " + " Rezerve eden: " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " Telefon Numarası: " + row["Telefon"].ToString() + " Saha Adı: " + row["Saha"].ToString();
                button.Tag = id;
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                //button.Click += new EventHandler(Button_Click);
                button.Click += new EventHandler(Button_Delete);

                //button.Click += new EventHandler(button3_Click);


                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.ForeColor = Color.Beige;
                    button.BackColor = Color.Green;
                    //button.FlatAppearance.BorderColor = Color.Green;
                    button.FlatAppearance.BorderSize = 2;




                }

                if (row["Durum"].ToString() == "Beklemede")
                {


                    button.ForeColor = Color.Red;
                }
                // Butonu panele ekliyoruz.
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }



        #endregion



        #region Sunday

        public void GetListAppoitnmentSunday(Panel panel)
        {

            DataTable myDataTable = new DataTable();

            string query = "SELECT * FROM Randevutbl WHERE RandevuGunu= 'Pazar' and RandevuTarihi >= @today   ORDER BY RandevuSaati ASC,RandevuTarihi asc";
            DateTime today = DateTime.Today;
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@today", today);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(myDataTable);

            //Panel panel = new Panel();
            //  panel2.Dock = DockStyle.Fill;

            // DataTable nesnesindeki her satır için bir buton oluşturuyoruz.
            int buttonWidth = 150;
            int buttonHeight = 50;
            int y = 10;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                DataRow row = myDataTable.Rows[i];

                // Buton nesnesini oluşturuyoruz.

                DateTime date = (DateTime)row["RandevuTarihi"];
                int id = (int)row["Id"];
                Button button = new Button();
                button.Text = "Rezerve Tarihi:" + date.ToString("dd.MM.yyyy") + "     " + "Rezerve Saati" + row["RandevuSaati"] + "   " + " Rezerve eden: " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " Telefon Numarası: " + row["Telefon"].ToString() + " Saha Adı: " + row["Saha"].ToString();


                button.Tag = id;
                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                //button.Click += new EventHandler(Button_Click);
                //button.Click += new EventHandler(button3_Click);
                button.Click += new EventHandler(Button_Delete);





                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.ForeColor = Color.Beige;
                    button.BackColor = Color.Green;
                    //button.FlatAppearance.BorderColor = Color.Green;
                    button.FlatAppearance.BorderSize = 2;




                }

                if (row["Durum"].ToString() == "Beklemede")
                {


                    button.ForeColor = Color.Red;
                }
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

        

        #endregion

      

        private void button2_Click_1(object sender, EventArgs e)
        {
            Member member = new Member();


            member.Show();
        }

     

    

        private void button4_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        //Delete Button

        private void Button_Delete(object sender, EventArgs e)
        {
          
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Tag);
            this.panel9.Visible = true;


  

            textBox1.Text = button.Text;

            //string query = "delete from Randevutbl where Id = @id";

            //SqlCommand command = new SqlCommand(query, conn);
            //command.Parameters.AddWithValue("@id", id);



                    //if (MessageBox.Show("Bu kaydı silmek istediğinizden emin misiniz?", "Kaydı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    conn.Open();
                    //    int result = command.ExecuteNonQuery();
                    //    conn.Close();

                    //    if (result > 0)
                    //    {
                    //        MessageBox.Show("Randevu Silindi");
                    //        panel9.Visible = false;
                    //        buttonid = -1;



                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Randevu Silinemedi");
                    //        buttonid = -1;
                    //}
               




            }

     


    }
}



