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
    public partial class FrmBilgiDüzenle : Form
    {
        public FrmBilgiDüzenle()
        {
            InitializeComponent();
        }
        public string TCno;

        Class1 bgl = new Class1();
        private void FrmBilgiDüzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;
            SqlCommand komut = new SqlCommand("Select * from Tbl_Hastalar where HastaTC = @p1",bgl.Connection());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskGsm.Text = dr[4].ToString();
                TxtSıfre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.Connection().Close();
        }

        private void BtnDüzenle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Update Tbl_Hastalar set HastaAd=@p1, HastaSoyad=@p2, HastaTC=@p3, HastaTelefon=@p4, HastaSifre=@p5, HastaCinsiyet=@p6 where HastaTC=@p3", bgl.Connection());
            komut1.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut1.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@p3", MskTC.Text);
            komut1.Parameters.AddWithValue("@p4", MskGsm.Text);
            komut1.Parameters.AddWithValue("@p5", TxtSıfre.Text);
            komut1.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
            komut1.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Bilgiler Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
