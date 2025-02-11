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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        
        Class1 bgl = new Class1();  
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter dv1 = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.Connection());
            dv1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@p1)", bgl.Connection());
            komut.Parameters.AddWithValue("@p1", TxtBrans.Text);
            komut.ExecuteNonQuery();
            bgl.Connection().Close();   
            MessageBox.Show("Branş Eklendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Delete Tbl_Branslar Where BransAd=@p1", bgl.Connection());
            komut1.Parameters.AddWithValue("@p1", TxtBrans.Text);
            komut1.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Seçilen kayıt silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update Tbl_Branslar set BransAd=@p1 where Bransid=@p2", bgl.Connection());
            komut2.Parameters.AddWithValue("@p1", TxtBrans.Text);
            komut2.Parameters.AddWithValue("@p2", Txtid.Text);
            komut2.ExecuteNonQuery();
            bgl.Connection().Close();
            MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information );
        }
    }
}
