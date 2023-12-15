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
            ExecuteReadProcedure(conn);
            ListCategories(conn);
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

        var rows = connection.Execute(updateQry, new
        {
            id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
            title = "Frontend"
        });

        Console.WriteLine($"{rows} registros atualizados");

    }
    static void DeleteCategory(SqlConnection connection)
    {
        var deleteQuery = "DELETE [Category] WHERE [Id]=@id";
        var rows = connection.Execute(deleteQuery, new
        {
            id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
        });

        Console.WriteLine($"{rows} registros excluídos");
    }

    static void CreateManyCategories(SqlConnection connection)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Google Cloud Platform",
            Url = "GCP",
            Description = "Carreira destinada a serviços do Google Cloud Platform",
            Order = 10,
            Summary = "GCP",
            Featured = false,
        };

        var category2 = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Magalu Cloud",
            Url = "MC",
            Description = "Carreira destinada a serviços do Magalu Cloud",
            Order = 9,
            Summary = "MC",
            Featured = true,
        };

        var category3 = new Category
        {
            Id = Guid.NewGuid(),
            Title = "CloudFire",
            Url = "CF",
            Description = "Carreira destinada a serviços do CloudFire",
            Order = 11,
            Summary = "CF",
            Featured = true,
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

        var rows = connection.Execute(insertSQL, new[]{

        new {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        },
        new {
            category2.Id,
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured
        },
        new {
            category3.Id,
            category3.Title,
            category3.Url,
            category3.Summary,
            category3.Order,
            category3.Description,
            category3.Featured
        }
        }
        );
        Console.WriteLine($"{rows} Linhas inseridas");

    }

    static void ExecuteProcedure(SqlConnection connection)
    {
        var procedure = "spDeleteStudent";

        var param = new {StudentId = "47d015f6-da2c-4934-a55a-61cfb542e154"};

        var rows = connection.Execute(procedure, param, commandType: CommandType.StoredProcedure);

        Console.WriteLine($"{rows} linhas afetadas");
    }

    static void ExecuteReadProcedure(SqlConnection connection)
    {
        var procedure = "spGetCoursesByCategory";

         var param = new {CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142"};

        var courses = connection.Query<Category>(procedure, param, commandType: CommandType.StoredProcedure);

        foreach(var item in courses)
        {
            Console.WriteLine(item.Title);
        }
    }
}
