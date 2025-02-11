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

namespace Proje_Hastane
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        Class1 bgl=new Class1();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;
            
            // Ad-Soyad Çekme : 
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1", bgl.Connection());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.Connection().Close();

            // Randevu Geçmişi :
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevu where HastaTC=@tc", bgl.Connection());
            da.SelectCommand.Parameters.AddWithValue("@tc", tc);

            da.Fill(dt);
            dataGridView2.DataSource = dt;

            //Branşları Çekme : 
            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.Connection());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.Connection().Close();

            
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.Connection());
            komut3.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.Connection().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevu where RandevuBrans='" + CmbBrans.Text +"'" + "and RandevuDoktor= '" + CmbDoktor.Text+ "' and RandevuDurum=0" ,bgl.Connection());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDüzenle frm = new FrmBilgiDüzenle();
            frm.TCno = LblTC.Text;
            frm.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtıd.Text = dataGridView1.SelectedCells[0].Value.ToString();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("Update Tbl_Randevu Set RandevuDurum=1, HastaTC=@p1, HastaSikayet=@p2 where Randevuid=@p3", bgl.Connection());
            komut4.Parameters.AddWithValue("@p1", LblTC.Text);
            komut4.Parameters.AddWithValue("p2", RchTxtSikayet.Text);
            komut4.Parameters.AddWithValue("@p3", Txtıd.Text);
            komut4.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Randevu Alındı", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
