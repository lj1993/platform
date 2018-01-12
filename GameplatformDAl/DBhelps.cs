using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GameplatformDAl
{
    public class Dbhelps
    {
        private readonly string sql =ConfigurationManager.ConnectionStrings["GPF"].ToString();
        private SqlConnection connect;

        public SqlConnection Connect
        {
            get {
                if (connect == null)
                {
                    connect = new SqlConnection(sql);
                }
                return connect;
            }
            
        }

        public void OpenConnect()
        {
            if(Connect.State==ConnectionState.Closed){
                Connect.Open();
            }else if(Connect.State==ConnectionState.Broken){
                Connect.Close();
                connect.Open();
            }
        }

        public void CloseConnect()
        {
            if (Connect.State == ConnectionState.Open || Connect.State == ConnectionState.Broken)
            {
                Connect.Close();
            }
        }

    }
}
