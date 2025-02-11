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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
         
        Class1 bgl = new Class1();
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCNO;
            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar Where DoktorTC=@p1", bgl.Connection());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader dc = komut.ExecuteReader();
            while (dc.Read())
            {
                TxtAd.Text = dc[1].ToString();
                TxtSoyad.Text = dc[2].ToString();   
                CmbBrans.Text = dc[3].ToString();
                TxtSifre.Text = dc[5].ToString();   
            }
            
        }

        private void BtnDüzenle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Update Tbl_Doktorlar Set DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3, DoktorSifre=@p5 where DoktorTC=@p4", bgl.Connection());
            komut1.Parameters.AddWithValue("@p1", TxtAd.Text);  
            komut1.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut1.Parameters.AddWithValue("@p4", MskTC.Text);
            komut1.Parameters.AddWithValue("@p5", TxtSifre.Text);
            komut1.ExecuteNonQuery();
            MessageBox.Show("Seçilen Kayıt Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            bgl.Connection().Close();
        }
    }
}
