using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess;

    class Program{


        static void Main(string[] args)
        {
            const string connectionString = 
            $"Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";
            
            using (var conn = new SqlConnection(connectionString))
            {
                //Abrir conexão
                conn.Open();

                //Executar comando SQL
                using(var cmd = new SqlCommand())
                {
                    //Setar conexão utilizada
                    cmd.Connection = conn;
                    //Setar tipo do comando a ser executado
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT [Id], [Title] FROM [Category]";
                    var reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                    }
                }
            }
           
            

            Console.WriteLine(args);
        }
    }
