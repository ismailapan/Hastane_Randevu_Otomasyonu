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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        Class1 bgl = new Class1();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
          SqlCommand komut = new SqlCommand("Select * from Tbl_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2", bgl.Connection());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                SekreterDetay fr = new SekreterDetay();
                fr.TCno = MskTC.Text;
                fr.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("T.C Kimlik No Veya Şifre Hatalıdır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.Connection().Close();
        }
    }
}
