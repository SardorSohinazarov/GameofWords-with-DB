using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So_z_O_yin.Broker;

internal class CreatingWordsDB
{
    static async Task FileToDB()
    {
        string path = "C:\\Users\\Sardor Sohinazarov\\OneDrive\\Desktop\\UzbekWordsBase.txt";
        var listofwords = await File.ReadAllLinesAsync(path);
        for (int i = 0; i < listofwords.Length; i++)
        {
            if (listofwords[i] != "")
                await CreatAnyThing(listofwords[i]);
        }
    }

    public static async Task CreatAnyThing(string word)
    {
        SqlParameter sqlParameter = new SqlParameter("@word", word);
        string connectionString = @"Server=(localdb)\mssqllocaldb; Database=UzbekWordsBase;";

        using (var connection = new SqlConnection(connectionString))
        {
            string query = $"insert into Words(Word) values(@word)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(sqlParameter);

            await connection.OpenAsync();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            Console.WriteLine(dataReader.ToString());
        }
    }
}