using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class SekreterDetay : Form
    {
        public SekreterDetay()
        {
            InitializeComponent();
        }
        Class1 bgl = new Class1();
        public string TCno;
        private void SekreterDetay_Load(object sender, EventArgs e)
        {
            label2.Text = TCno;

            // Ad-Soyad Çekme

            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1",bgl.Connection());
            komut.Parameters.AddWithValue("@p1", label2.Text);
            SqlDataReader dr1 = komut.ExecuteReader();
            while (dr1.Read())
            {
               label4.Text = dr1[0]+ " ";
            }
            bgl.Connection().Close();

            //Branlşarı çekme

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar", bgl.Connection());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Doktorları çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter dv = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar', DoktorBrans from Tbl_Doktorlar", bgl.Connection());
            dv.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşları çekme
            SqlCommand komut1 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.Connection());
            SqlDataReader dr2 = komut1.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }

            // Doktorları Çekme
            SqlCommand komut2 = new SqlCommand("Select DoktorAd, DoktorSoyad from Tbl_Doktorlar", bgl.Connection());
            SqlDataReader dv2 = komut2.ExecuteReader();
            while (dv2.Read())
            {
                comboBox1.Items.Add(dv2[0] + " " + dv2[1]);
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevu (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)", bgl.Connection());
            komutkaydet.Parameters.AddWithValue("@p1", MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2", MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4", comboBox1.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Randevu Alınmıştır", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update Tbl_Randevu set RandevuTarih=@p1, RandevuSaat=@p2, RandevuBrans =@p3, RandevuDoktor=@p4 where HastaTC=@p5", bgl.Connection());
            komut2.Parameters.AddWithValue("@p1",MskTarih.Text);
            komut2.Parameters.AddWithValue("@p2", MskSaat.Text);
            komut2.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@p4", comboBox1.Text);
            komut2.Parameters.AddWithValue("@p5", MskTc.Text);
            komut2.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Randevu Güncellendi", "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1", bgl.Connection());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader da = komut3.ExecuteReader();
            while(da.Read())
            {
                comboBox1.Items.Add(da[0] + " " + da[1]);
            }
            bgl.Connection().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@p1)", bgl.Connection());
            komut4.Parameters.AddWithValue("@p1", richTextBox1.Text);
            komut4.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Duyuru oluşturuldu.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fr1 = new FrmDoktorPaneli();
            fr1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBrans fr2 = new FrmBrans();
            fr2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmRandevuList frl = new FrmRandevuList();  
            frl.Show();
        }
    }
}
