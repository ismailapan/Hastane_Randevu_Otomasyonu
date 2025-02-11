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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        public string TC;

        Class1 bgl = new Class1();
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            //Ad-Soyad Çekme 
            SqlCommand komut = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar Where DoktorTC=@p1", bgl.Connection());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.Connection().Close();

            // Randevu 
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevu where RandevuDoktor='" + LblAdSoyad.Text + "'", bgl.Connection());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDuyurular frt = new FrmDuyurular();
            frt.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle fg = new FrmDoktorBilgiDuzenle();
            fg.TCNO=LblTC.Text;
            fg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDoktorDetay ft = new FrmDoktorDetay();
            ft.Close();
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            richTextBox1.Text = dataGridView1.SelectedCells[7].Value.ToString();    
        }
    }
}
