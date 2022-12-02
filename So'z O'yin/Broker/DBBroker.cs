using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So_z_O_yin.Broker;

internal class DBBroker
{
    public static async Task<string> OneWord(string word)
    {
        SqlParameter sqlParameter = new SqlParameter("@word", word);
        string connectionString = @"Server=(localdb)\mssqllocaldb; Database=UzbekWordsBase;";

        using (var connection = new SqlConnection(connectionString))
        {
            string query = $"select top(1) word from Words where word like '{word[word.Length-1]}%' and word not in (select word from ExtraTable)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(sqlParameter);

            await connection.OpenAsync();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            string oneword = "";
            while (await dataReader.ReadAsync())
            {
                oneword = dataReader[0].ToString();
            }
            return oneword;
        }
    }

    public static async Task<string> InsertToExtraDBUsedWords(string word)
    {
        SqlParameter sqlParameter = new SqlParameter("@word", word);
        string connectionString = @"Server=(localdb)\mssqllocaldb; Database=UzbekWordsBase;";

        using (var connection = new SqlConnection(connectionString))
        {
            string query = $"insert into ExtraTable(word) values(@word)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(sqlParameter);

            await connection.OpenAsync();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            string oneword = "";
            while (await dataReader.ReadAsync())
            {
                oneword = dataReader[0].ToString();
            }
            return oneword;
        }
    }
    public static async Task CleanDBFromWords()
    {
        string connectionString = @"Server=(localdb)\mssqllocaldb; Database=UzbekWordsBase;";

        using (var connection = new SqlConnection(connectionString))
        {
            string query = $"delete from Extratable";
            SqlCommand command = new SqlCommand(query, connection);

            await connection.OpenAsync();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            string oneword = "";
            while (await dataReader.ReadAsync())
            {
                oneword = dataReader[0].ToString();
            }
            Console.WriteLine("Kesh tozalandi");
        }
    }
    public static async Task<List<string>> ListOfUsedWords(string word)
    {
        SqlParameter sqlParameter = new SqlParameter("@word", word);
        string connectionString = @"Server=(localdb)\mssqllocaldb; Database=UzbekWordsBase;";

        using (var connection = new SqlConnection(connectionString))
        {
            string query = $"select word from ExtraTable";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(sqlParameter);

            await connection.OpenAsync();

            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            List<string> list = new List<string>();
            while (await dataReader.ReadAsync())
            {
                list.Add(dataReader[0].ToString());
            }
            return list;
        }
    }
}