using BookStoreAPI.Models;
using BookStoreAPI.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;

namespace BookStoreAPI.Repositories
{
    public class BookMysqlDB(IConfiguration configuration) : IBookRepository
    {
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<ICollection<Book>> GetAllAsync()
        {
            var bookList = new List<Book>();
            await using MySqlConnection conn = new(_connectionString);
            conn.Open();

            var query = "SELECT * FROM Book";
            MySqlCommand cmd = new(query, conn);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var person = new Book()
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    PublicationYear = reader.GetInt32(3),
                    ISBN = reader.GetString(4),
                    InStock = reader.GetInt32(5),
                };
                bookList.Add(person);
            }
            return bookList;
        }
    }
}
