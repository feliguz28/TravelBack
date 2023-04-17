using Microsoft.Data.SqlClient;
using Travel.Utilities;

namespace Travel.Connection
{
    public class ConnectionDB
    {
        public static SqlConnection Conecction()
        {
            SqlConnection con = new(ConfigConst.StringConnection());
            con.Open();

            return con;
        }
    }
}
