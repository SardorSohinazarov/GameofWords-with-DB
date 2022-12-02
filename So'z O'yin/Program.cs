using So_z_O_yin.Broker;
using So_z_O_yin.Service;
using System.Data.SqlClient;
using System.IO;

internal class Program
{
    static async Task Main()
    {
        await GameService.Game();
    }
}

class Testing
{
    static void Test()
    {
        SqlConnection conn = null;
        SqlDataReader reader = null;

        string inputCity = "London";
        try
        {
            conn = new SqlConnection("Server=(local);DataBase=Northwind;Integrated Security=SSPI");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Customers where city = @City", conn);

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@City";
            param.Value = inputCity;

            cmd.Parameters.Add(param);

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0}, {1}",
                    reader["CompanyName"],
                    reader["ContactName"]);
            }
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }
        }
    }

}
