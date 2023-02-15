using HalıSaha.Models;
using Microsoft.Win32;
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
    public partial class Register : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=176.236.132.247;Initial Catalog=HaliSahaDb;User Id=sa;Password=XjsqEEWdvP17pMe");
        Randevu randevu = new Randevu();
       

        public Register()
        {
            InitializeComponent();
            GetComboboxValue();
        }
        private void Register_Load(object sender, EventArgs e)
        {

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
                randevu.gun = comboBox3.SelectedItem == null ? "Seçilmedi" : comboBox3.SelectedItem.ToString();
                randevu.durum = comboBox4.SelectedItem == null ? "Seçilmedi" : comboBox4.SelectedItem.ToString();
                randevu.telefon = textBox3.Text;
           


            try
            {
                conn.Open();

                string query = "insert into Randevutbl (Ad,Soyad,Saha,RandevuGunu,RandevuSaati,Durum,Telefon) values (@ad,@soyad,@saha,@randevugunu,@randevusaati,@durum,@telefon)";


                using (SqlCommand command = new SqlCommand(query, conn))

                {
                    command.Parameters.AddWithValue("@ad", randevu.ad);
                    command.Parameters.AddWithValue("@soyad", randevu.Soyad);
                    command.Parameters.AddWithValue("@saha", randevu.saha);
                    command.Parameters.AddWithValue("@randevugunu", randevu.gun);
                    command.Parameters.AddWithValue("@randevusaati", randevu.saat);

                    command.Parameters.AddWithValue("@durum", randevu.durum);
                    command.Parameters.AddWithValue("@telefon", textBox3.Text);

                    command.ExecuteNonQuery();
                }


                conn.Close();



                MessageBox.Show("Başarıyla Kayıt Edildi");
            }
            catch
            {

                MessageBox.Show("Bir hata oluştu");
            }

           

        }




        private void GetComboboxValue()
        {
            comboBox1.Items.Add("A1");
            comboBox1.Items.Add("C1");
            comboBox1.Items.Add("C3");


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
            comboBox3.Items.Add("Pazartesi");
            comboBox3.Items.Add("Salı");
            comboBox3.Items.Add("Carşamba");
            comboBox3.Items.Add("Perşembe");
            comboBox3.Items.Add("Cuma");
            comboBox3.Items.Add("Cumartesi");
            comboBox3.Items.Add("Pazar");

            //
            comboBox4.Items.Add("Rezerve");
            comboBox4.Items.Add("Beklemede");
        





        }

        


    }
}
