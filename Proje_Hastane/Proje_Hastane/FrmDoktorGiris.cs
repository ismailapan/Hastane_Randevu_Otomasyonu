using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        Class1 bgl = new Class1();

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar Where DoktorTC=@p1 and DoktorSifre=@p2", bgl.Connection());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);  
            SqlDataReader dw = komut.ExecuteReader();
            if (dw.Read())
            {
                FrmDoktorDetay fd = new FrmDoktorDetay();
                fd.TC = maskedTextBox1.Text;
                fd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("TC Kimlik Numarası Veya Şifre Hatalıdır.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            bgl.Connection().Close();
        }
    }
}
