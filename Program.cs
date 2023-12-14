using System;
using System.Data;
using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

    class Program{


        static void Main(string[] args)
        {
            const string connectionString = 
            $"Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";
            
            using (var conn = new SqlConnection(connectionString))
            {
                var categories = conn.Query<Category>("SELECT [Id], [Title] FROM [Category]");

                foreach(var category in categories)
                {
                    Console.WriteLine($"{category.Id} - {category.Title}");
                }
            }
           
        }
    }
