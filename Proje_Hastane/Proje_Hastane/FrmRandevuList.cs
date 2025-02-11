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
    public partial class FrmRandevuList : Form
    {
        public FrmRandevuList()
        {
            InitializeComponent();
        }

        Class1 bgl = new Class1();
        private void FrmRandevuList_Load(object sender, EventArgs e)
        {
            DataTable rl = new DataTable();
            SqlDataAdapter ra = new SqlDataAdapter("Select * from Tbl_Randevu", bgl.Connection());
            ra.Fill(rl);
            dataGridView1.DataSource = rl;
        }

       
        
            
        
    }
}
