using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Proje_Hastane
{
    internal class Class1
    {
        public SqlConnection Connection() 
        {
            SqlConnection baglan = new SqlConnection("Data Source=LAPTOP9IICM93V\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
