using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace HastaTakip
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection("provider= microsoft.ace.oledb.12.0; data source= hastaKayitDB.accdb ");
        OleDbCommand cmd;
        OleDbDataAdapter da;

        void listele()
        {
            da=new OleDbDataAdapter("select * from hastalar ", con);
            DataTable tablo= new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource=tablo;

            temizle();
        }

        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            textBox6.Text = "";
            dateTimePicker2.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        public Form1()
        {
            InitializeComponent();
        }

      
        
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            comboBox1.Items.Add("Erkek");
            comboBox1.Items.Add("Kadın");
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void label7_Click(object sender, EventArgs e)
        {

        }
        
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("insert into hastalar (TC, AD, SOYAD, CINSIYET, TELEFON, ADRES, DTARIH,GTARIH,CTARIH)values (@tc, @ad, @soyad, @cinsiyet, @telefon, @adres, @dtarih,@gtarih,@ctarih) ", con);
                cmd.Parameters.AddWithValue("@tc", textBox2.Text);
                cmd.Parameters.AddWithValue("@ad", textBox3.Text);
                cmd.Parameters.AddWithValue("@soyad", textBox10.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
                cmd.Parameters.AddWithValue("@telefon", textBox4.Text);
                cmd.Parameters.AddWithValue("@adres", textBox6.Text);
                cmd.Parameters.AddWithValue("@dtarih", dateTimePicker2.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@gtarih", dateTimePicker1.Value.ToString());
                cmd.Parameters.AddWithValue("@ctarih", dateTimePicker3.Value.ToString());
                if(textBox3.Text==""|| textBox2.Text== ""|| comboBox1.Text==""|| textBox10.Text=="")
                {
                    MessageBox.Show("Zorunlu alan boş bırakılamaz!");
                }

                if (textBox2.Text.Length != 11)
                {
                    MessageBox.Show("TC 11 haneli olmalı!");
                }
                else
                {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Yeni bir kayıt eklendi.");
                }

                
            }
            catch(Exception)
            {
                MessageBox.Show("Hatalı işlem !");
            }
            listele();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand(" delete from hastalar where TC= @tc ", con);
            cmd.Parameters.AddWithValue("@tc", textBox2.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Silme işlemi tamam ");
            listele();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            da = new OleDbDataAdapter("select* from hastalar where tc  like '" + textBox9.Text+ "%'", con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            
            textBox10.Text= dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("update hastalar set AD= ' " +textBox3.Text+ "', SOYAD= '" + textBox10.Text + "', CINSIYET= '" + comboBox1.Text + "', TELEFON= '" +textBox4.Text+ "', ADRES= '"+textBox6.Text+ "', UCRET= '" + dateTimePicker3.Value.ToString() + "',GTARIH= '" +dateTimePicker1.Value.ToString()+ "', CTARIH= '" +dateTimePicker3.Value.ToString()+ "' where TC= '"+textBox2.Text +"' ", con);
            
            con.Open();
            int sonuc=cmd.ExecuteNonQuery();
            con.Close();
            if (sonuc == 1)
                MessageBox.Show("Güncelleme başarıyla tamamlandı.");
            else
                MessageBox.Show("Güncelleme başarısız.");


            listele();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
