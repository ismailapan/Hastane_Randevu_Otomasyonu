using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmHastaGiriş : Form
    {
        public FrmHastaGiriş()
        {
            InitializeComponent();
        }
        Class1 bgl =new Class1();
        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr2 = new FrmHastaKayit();
            fr2.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar Where HastaTC=@p1 and HastaSifre=@p2", bgl.Connection());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmHastaDetay frm = new FrmHastaDetay();
                frm.tc=MskTC.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("T.C. Kimlik No veya Şifre Hatalıdır", "UYARI !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bgl.Connection().Close();
        }
    }
}
