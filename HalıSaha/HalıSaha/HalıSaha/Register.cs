using HalıSaha.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalıSaha
{
    public partial class Register : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=xx;Initial Catalog=HaliSahaDb;");
        Randevu randevu = new Randevu();
       

        public Register()
        {
            InitializeComponent();
            dateTimePicker2.Value = DateTime.Today;
            //GetTimeListbox();
            GetComboboxValue();
        
        }
        private void Register_Load(object sender, EventArgs e)
        {
            //GetTimeListbox();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text ) )
            {
                MessageBox.Show("Lütfen Ad ve Soyad alanlarını doldurunuz.", "Uyarı");
                return;
            }
           
                randevu.ad = textBox1.Text;
                randevu.Soyad = textBox2.Text;
                randevu.saha = comboBox1.SelectedItem == null ? "Seçilmedi" : comboBox1.SelectedItem.ToString();

                randevu.saat = comboBox2.SelectedItem == null ? "Seçilmedi" : comboBox2.SelectedItem.ToString();
               
                randevu.durum = comboBox4.SelectedItem == null ? "Seçilmedi" : comboBox4.SelectedItem.ToString();
                randevu.telefon = textBox3.Text;


            int RegisterCountControl = RandevuKayıtKontrol();
       if (RegisterCountControl >0 )
            {
                MessageBox.Show("Rezerve Edilmiş");
            }else
            {
                try
                {

                    //int day = dateTimePicker1.Value.Day;
                    DateTime selectedDate = dateTimePicker1.Value;

                    string formatDate = selectedDate.ToString("dddd", new CultureInfo("tr-TR"));
                    conn.Open();

                    string query = "insert into Randevutbl (Ad,Soyad,Saha,RandevuGunu,RandevuSaati,Durum,Telefon,RandevuTarihi) values (@ad,@soyad,@saha,@randevugunu,@randevusaati,@durum,@telefon,@randevutarihi)";

                    // DateTime selectdate = dateTimePicker1.Value;
                    using (SqlCommand command = new SqlCommand(query, conn))

                    {
                        command.Parameters.AddWithValue("@ad", randevu.ad);
                        command.Parameters.AddWithValue("@soyad", randevu.Soyad);
                        command.Parameters.AddWithValue("@saha", randevu.saha);
                        command.Parameters.AddWithValue("@randevugunu", formatDate);
                        command.Parameters.AddWithValue("@randevusaati", randevu.saat);

                        command.Parameters.AddWithValue("@durum", randevu.durum);
                        command.Parameters.AddWithValue("@telefon", textBox3.Text);

                        command.Parameters.AddWithValue("@randevutarihi", selectedDate);
                        command.ExecuteNonQuery();
                    }


                    conn.Close();



                    MessageBox.Show("Başarıyla Kayıt Edildi");

                }
                catch (Exception exception)
                {

                    MessageBox.Show(exception.Message, "Bir hata oluştu");
                }
            }

          

           

        }


        public int RandevuKayıtKontrol()
        {
          

        
            DateTime selectedDate = dateTimePicker1.Value;
            string formatDate = selectedDate.ToString("dddd", new CultureInfo("tr-TR"));
            conn.Open();
            string query = "select count(*) from Randevutbl where Saha = @saha and RandevuGunu = @randevugunu and RandevuSaati =@randevusaati " ;

            SqlCommand command = new SqlCommand(query, conn);

          
                 command.Parameters.AddWithValue("@saha", comboBox1.SelectedItem == null ? "Seçilmedi" : comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@randevugunu", formatDate);
            command.Parameters.AddWithValue("@RandevuSaati", comboBox2.SelectedItem == null ? "Seçilmedi" : comboBox2.SelectedItem.ToString());
            int RandevuSay =(int)command.ExecuteScalar();
            conn.Close() ;


            return RandevuSay;


        }

        private void GetComboboxValue()
        {
            comboBox1.Items.Add("A1");
            comboBox1.Items.Add("A2");
            comboBox1.Items.Add("A3");


            comboBox2.Items.Add("09.00- 10.00");
            comboBox2.Items.Add("10.00- 11.00");
            comboBox2.Items.Add("11.00- 12.00");
            comboBox2.Items.Add("12.00- 13.00");
            comboBox2.Items.Add("13.00- 14.00");
            comboBox2.Items.Add("14.00- 15.00");
            comboBox2.Items.Add("15.00- 16.00");
            comboBox2.Items.Add("16.00- 17.00");
            comboBox2.Items.Add("17.00- 18.00");
            comboBox2.Items.Add("18.00- 19.00");
            comboBox2.Items.Add("19.00- 20.00");
            comboBox2.Items.Add("20.00- 21.00");
            comboBox2.Items.Add("21.00- 22.00");
            comboBox2.Items.Add("22.00- 23.00");
            comboBox2.Items.Add("23.00- 24.00");
            comboBox2.Items.Add("24.00- 01.00");
            comboBox2.Items.Add("01.00- 02.00");
            comboBox2.Items.Add("02.00- 03.00");
            comboBox2.Items.Add("03.00- 04.00");
            comboBox2.Items.Add("04.00- 05.00");

            //
            

            //
            comboBox4.Items.Add("Rezerve");
            comboBox4.Items.Add("Beklemede");
        





        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();


                if(string.IsNullOrEmpty(searchValue))
            {
                //

            }else
            {
                SearchData(searchValue);
                SearchDataRandevu(searchValue);
            }

               


        }


       public void SearchData(string seachValue)
        {
            string query = "select * from Member  where Name like @SearchValue or Surname like @SearchValue";

            SqlCommand command= new SqlCommand(query,conn);

            conn.Open();

            command.Parameters.AddWithValue("@SearchValue", "%" + seachValue + "%");


            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable= new DataTable();

            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            conn.Close();
        }

        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{


        //    if (e.ColumnIndex == 0 && e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
        //    {
        //        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];







        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (row.Index != e.RowIndex)
        //            {
        //                DataGridViewCheckBoxCell chk2 = (DataGridViewCheckBoxCell)row.Cells[0];
        //                chk2.Value = chk2.FalseValue;
        //            }
        //        }

        //        string state = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

        //        if (state == "Yasaklı")
        //            MessageBox.Show("Bu kişi Yasaklanmıştır", "Yasaklı Üye");
        //        if (chk.Value == chk.TrueValue && state != "Yasaklı")
        //        {
        //            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        //            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        //            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        //            chk.Value = chk.TrueValue;
        //            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
        //        }
        //        else if (chk.Value == chk.FalseValue)
        //        {
        //            textBox1.Clear();
        //            textBox2.Clear();
        //            textBox3.Clear();
        //            chk.Value = chk.FalseValue;
        //            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
        //        }
        //    }
        //    else
        //    {
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
        //            chk.Value = chk.FalseValue;
        //            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
        //        }
        //        textBox1.Clear();
        //        textBox2.Clear();
        //        textBox3.Clear();


        //    }

        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index != e.RowIndex)
                    {
                        DataGridViewCheckBoxCell chk2 = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk2.Value = chk2.FalseValue;
                    }
                }

                if (dataGridView1.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    string state = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                    if (state == "Yasaklı")
                    {
                        MessageBox.Show("Bu kişi Yasaklanmıştır", "Yasaklı Üye");
                    }

                    else if (chk.Value == chk.TrueValue)
                    {
                        textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        if (dataGridView1.Rows.Count > 0 && e.RowIndex < dataGridView1.Rows.Count)
                        {
                            // satır indeksi geçerli ise işlemler yapılır
                            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
                        }
                        if (dataGridView1.Rows.Count > 0 && e.RowIndex < dataGridView1.Rows.Count)
                        {
                            // satır indeksi geçerli ise işlemler yapılır
                            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                        }

                        chk.Value = chk.FalseValue;
                        if (dataGridView1.Rows.Count > 0 && e.RowIndex < dataGridView1.Rows.Count)
                        {
                            // satır indeksi geçerli ise işlemler yapılır
                            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        }

                    }
                    else if (chk.Value == chk.FalseValue)
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        chk.Value = chk.FalseValue;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    }
                }

                
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = chk.FalseValue;
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                }

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex ==0) // burada Checkbox sütununun sütun dizinini belirleyin
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
              
                if (e.ColumnIndex == 0) // Burada 0, CheckBox sütununun sıfır tabanlı ColumnIndex değeridir.
                    {
                        int rowIndex = e.RowIndex;
                        DataGridViewRow row = dataGridView2.Rows[e.RowIndex];



                    if (row.Cells[2].Value != null)
                    {
                        label11.Text = row.Cells[2].Value.ToString();
                    }
                    else
                    {
                        label11.Text = "";
                    }

                    if (row.Cells[1].Value != null)
                    {
                        label10.Text = row.Cells[1].Value.ToString();
                    }
                    else
                    {
                        label10.Text = "";
                    }


                    if (row.Cells[2].Value != null)
                        {
                            textBox1.Text = row.Cells[2].Value.ToString();
                        }
                        else
                        {
                            textBox1.Text = "";
                        }

                        if (row.Cells[3].Value != null)
                        {
                            textBox2.Text = row.Cells[3].Value.ToString();
                        }
                        else
                        {
                            textBox2.Text = "";
                        }

                        if (row.Cells[4].Value != null)
                        {
                            comboBox1.Text = row.Cells[4].Value.ToString();
                        }
                        else
                        {
                            comboBox1.Text = "";
                        }

                        if (row.Cells[7].Value != null)
                        {
                            comboBox4.Text = row.Cells[7].Value.ToString();
                        }
                        else
                        {
                            comboBox4.Text = "";
                        }

                        if (row.Cells[6].Value != null)
                        {
                            comboBox2.Text = row.Cells[6].Value.ToString();
                        }
                        else
                        {
                            comboBox2.Text = "";
                        }

                        if (row.Cells[8].Value != null)
                        {
                            textBox3.Text = row.Cells[8].Value.ToString();
                        }
                        else
                        {
                            textBox3.Text = "";
                        }
                    

                    // burada verileri TextBox ve ComboBox'lara aktarabilirsiniz
                }
                else
                {
                    // CheckBox seçimi kaldırıldığında yapılacak işlemler
                    // burada TextBox ve ComboBox'ların değerlerini temizleyebilirsiniz
                }
            }
       


        }

        public void SearchDataRandevu(string seachValue)
        {
            string query = "select * from RandevuTbl  where Ad like @SearchValue or Soyad like @SearchValue or RandevuGunu = @SearchValue or Durum =@SearchValue or RandevuSaati=@SearchValue";

            SqlCommand command = new SqlCommand(query, conn);

            conn.Open();

            command.Parameters.AddWithValue("@SearchValue", "%" + seachValue + "%");


            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;

            conn.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox3.Text.Trim();


            if (string.IsNullOrEmpty(searchValue))
            {
                //

            }
            else
            {
                SearchData(searchValue);
                SearchDataRandevu(searchValue);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Lütfen Ad ve Soyad alanlarını doldurunuz.", "Uyarı");
                return;
            }
           
              
         
            int id = Convert.ToInt32(label10.Text);

            string Name = textBox1.Text.Trim();
            string Surname = textBox2.Text.Trim();
            string Saha = comboBox1.SelectedItem == null ? "Seçilmedi" : comboBox1.SelectedItem.ToString();
            string Clock = comboBox2.SelectedItem == null ? "Seçilmedi" : comboBox2.SelectedItem.ToString();
            DateTime selectedDate = dateTimePicker1.Value;


            string formatDate = selectedDate.ToString("dddd", new CultureInfo("tr-TR"));

            string State = comboBox4.SelectedItem == null ? "Seçilmedi" : comboBox4.SelectedItem.ToString();

            string Phone = textBox3.Text.Trim();
            //id
            
           

            string query = "update Randevutbl set Ad=@name ,Soyad =@soyad, Saha =@saha , RandevuGunu = @randevugunu,RandevuSaati = @randevusaati ,Durum=@durum,Telefon =@telefon where Id = @id";

            SqlCommand command = new SqlCommand(query,conn);


            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@soyad",Surname);
            command.Parameters.AddWithValue("@saha", Saha);
            command.Parameters.AddWithValue("@randevugunu", formatDate);
            command.Parameters.AddWithValue("@randevusaati", Clock);
            command.Parameters.AddWithValue("@durum",State);
            command.Parameters.AddWithValue("@telefon", Phone);
            command.Parameters.AddWithValue("@id",id);



            conn.Open();
             int rowsAffected = command.ExecuteNonQuery();

            conn.Close();
            if(rowsAffected >0 )
            {
                MessageBox.Show("Güncelleme İşlemi Başarıyla tamamlandı");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
               
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();    
                comboBox4.Items.Clear();
                
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }



        //public void GetTimeListbox()
        //{
        //    List<string> veriler = new List<string>();

        //    conn.Open();
        //    //hata var
        //    SqlCommand command = new SqlCommand("select * from Randevutbl",conn);

        //    SqlDataReader reader = command.ExecuteReader();

        //    while(reader.Read())
        //    {
        //        //listBox1.Items.Add(reader["RandevuSaati"].ToString());
        //        //veriler.Add(reader.GetString(5));
        //        Console.Write(reader.GetString(2));

        //    }
        //    conn.Close();

        //    Console.WriteLine(veriler);

        //    List<string> selectedCombobox = new List<string>();

        //    if (comboBox2.SelectedItem != null)
        //    {
        //         selectedCombobox.Add(comboBox2.SelectedItem.ToString());
        //        List<string> eslesenVeri = new List<string>();

        //        foreach (string veri in veriler)
        //        {

        //            foreach (string selected in selectedCombobox)
        //            {
        //                if (veri == selected)
        //                {
        //                    eslesenVeri.Add(veri);
        //                    break;
        //                }
        //            }
        //        }



        //        foreach (string eslesenveri in eslesenVeri)
        //        {
        //            listBox1.Items.Add(eslesenveri);
        //        }

        //    }






        //}

    
        public void GetClock()
        {
            try
            {
                DateTime selectedDate = dateTimePicker2.Value.Date;

                string formatDate = selectedDate.ToString("dddd", new CultureInfo("tr-TR"));

                string query = "select * from Randevutbl where cast(RandevuTarihi as date ) = cast(@randevutarihi as date)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@randevutarihi", selectedDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    listBox1.Items.Add(row["RandevuSaati"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            GetClock();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(label10.Text);
            string query = "delete from Randevutbl where Id= @id";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@id",id);

            if (MessageBox.Show("Bu Randevuyu silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Silme İşlemi başarılı.");
            }
           
        }
    }
}
