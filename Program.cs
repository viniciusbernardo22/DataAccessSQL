using System;
using System.Data;
using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

class Program
{


    static void Main(string[] args)
    {
        const string connectionString =
    $"Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

        using (var conn = new SqlConnection(connectionString))
        {
            UpdateCategory(conn);
        }

    }


    static void ListCategories(SqlConnection connection)
    {
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void CreateCategory(SqlConnection connection)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Amazon AWS",
            Url = "Amazon",
            Description = "Carreira destinada a serviços do AWS",
            Order = 8,
            Summary = "AWS Cloud",
            Featured = false,
        };

        var insertSQL = @"INSERT INTO 
            [Category] 
        VALUES(
            @Id, 
            @Title, 
            @Url, 
            @Summary, 
            @Order, 
            @Description,
            @Featured
            )";

        var rows = connection.Execute(insertSQL, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });
        Console.WriteLine($"{rows} Linhas inseridas");

    }

    static void UpdateCategory(SqlConnection connection)
    {
        var updateQry = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";

        var rows = connection.Execute(updateQry, new {
            id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
            title = "Frontend"
        });

        Console.WriteLine($"{rows} registros atualizados");

    }
}
