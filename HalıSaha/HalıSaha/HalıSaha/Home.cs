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

namespace HalıSaha
{
    public partial class Home : Form
    {

       

        #region SqlConnection
        SqlConnection conn = new SqlConnection("Data Source=176.236.132.247;Initial Catalog=HaliSahaDb;User Id=sa;Password=XjsqEEWdvP17pMe");
        #endregion

        //background

    

      




        public Home()
        {
            InitializeComponent();
            Getlist();



        }
        private void Home_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.ForestGreen;
          
            Getlist();

        }

     
        private void button1_Click(object sender, EventArgs e)
        {

            Register register = new Register();
            register.Show();
          

           
           
        }


       public void Getlist()
        {

            //string query = "select * from Randevutbl order by RandevuSaati asc";

            //SqlDataAdapter adapter = new SqlDataAdapter(query, conn);


            //DataTable table = new DataTable();
            //dataGridView1.DataSource = table;

            //adapter.Fill(table);
            //conn.Close();

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
        private void GetListAppoitnmentMonday()
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Pazartesi' order by RandevuSaati asc";

                SqlCommand command = new SqlCommand(query, conn);
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
               
                Button button = new Button();
                button.Text = row["RandevuSaati"] + " " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " " + row["Telefon"].ToString() + "  " + row["Saha"].ToString();

                button.Width = buttonWidth;
                button.Height = buttonHeight;
               

                
                button.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Arial", 14, FontStyle.Regular);
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                button.Click += new EventHandler(Button_Click);
                
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

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("Bilgiler: " + button.Text, "Detay");
        }
        #endregion



        #region thuesday

        private void GetListAppoitnmentThuesday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Salı' order by RandevuSaati asc";

            SqlCommand command = new SqlCommand(query, conn);
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

                Button button = new Button();
                button.Text = row["RandevuSaati"] + " " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " " + row["Telefon"].ToString() + "  " + row["Saha"].ToString();

                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                button.Click += new EventHandler(Button1_Click);

                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.BackColor = Color.Green;
                }
                if (row["Durum"].ToString() == "Beklemede")
                    button.ForeColor = Color.Red;
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("Bilgiler: " + button.Text, "Detay");
        }

        #endregion



        #region Wednesday

        private void GetListAppoitnmentWednesday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Carşamba' order by RandevuSaati asc";

            SqlCommand command = new SqlCommand(query, conn);
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

                Button button = new Button();
                button.Text = row["RandevuSaati"] + " " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " " + row["Telefon"].ToString() + "  " + row["Saha"].ToString();

                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                button.Click += new EventHandler(Button2_Click);

                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.BackColor = Color.Green;
                }
                if (row["Durum"].ToString() == "Beklemede")
                    button.ForeColor = Color.Red;
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show(" Bilgiler: " + button.Text,"Detay");
        }

        #endregion


        #region Thurday 

        private void GetListAppoitnmentThursday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Perşembe' order by RandevuSaati asc";

            SqlCommand command = new SqlCommand(query, conn);
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

                Button button = new Button();
                button.Text = row["RandevuSaati"] + " " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " " + row["Telefon"].ToString() + "  " + row["Saha"].ToString();

                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                button.Click += new EventHandler(Button3_Click);

                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.BackColor = Color.Green;
                }
                if (row["Durum"].ToString() == "Beklemede")
                    button.ForeColor = Color.Red;
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("Bilgiler: " + button.Text,"Detay");
        }

        #endregion


        #region Friday

        private void GetListAppoitnmentFriday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Cuma' order by RandevuSaati asc";

            SqlCommand command = new SqlCommand(query, conn);
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

                Button button = new Button();
                button.Text = row["RandevuSaati"] + " " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " " + row["Telefon"].ToString();

                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                button.Click += new EventHandler(Button4_Click);

                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.BackColor = Color.Green;
                }
                if (row["Durum"].ToString() == "Beklemede")
                    button.ForeColor = Color.Red;
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("Bilgiler: " + button.Text);
        }

        #endregion


        #region Saturday

        private void GetListAppoitnmentSaturday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Cumartesi' order by RandevuSaati asc";

            SqlCommand command = new SqlCommand(query, conn);
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

                Button button = new Button();
                button.Text = row["RandevuSaati"] + " " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " " + row["Telefon"].ToString() + "  " + row["Saha"].ToString() ;

                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                button.Click += new EventHandler(Button5_Click);

                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.BackColor = Color.Green;
                }
                if (row["Durum"].ToString() == "Beklemede")
                    button.ForeColor = Color.Red;
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("Bilgiler: " + button.Text);
        }

        #endregion



        #region Sunday

        private void GetListAppoitnmentSunday(Panel panel)
        {

            DataTable myDataTable = new DataTable();
            string query = "SELECT * FROM Randevutbl where RandevuGunu= 'Pazar' order by RandevuSaati asc";

            SqlCommand command = new SqlCommand(query, conn);
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

                Button button = new Button();
                button.Text = row["RandevuSaati"] + " " + row["Ad"].ToString() + " " + row["Soyad"].ToString() + " " + row["Telefon"].ToString() + "  " + row["Saha"].ToString();

                button.Width = buttonWidth;
                button.Height = buttonHeight;
                button.Top = (buttonHeight + 10) * i;
                button.Location = new Point(10, y);
                button.Click += new EventHandler(Button6_Click);

                if (row["Durum"].ToString() == "Rezerve")
                {
                    button.BackColor = Color.Green;
                }
                if (row["Durum"].ToString() == "Beklemede")
                    button.ForeColor = Color.Red;
                // Butonu panele ekliyoruz.
                panel.Controls.Add(button);
                y += 60;


            }

            conn.Close();
            // Paneli formumuza ekliyoruz.
            //this.Controls.Add(panel2);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show("Bilgiler: " + button.Text);
        }

        #endregion

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Member member = new Member();


            member.Show();
        }
    }
}



