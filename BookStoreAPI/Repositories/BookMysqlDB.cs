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

        public async Task<Book> AddAsync(Book book)
        {
            await using MySqlConnection conn = new(_connectionString);
            conn.Open();

            var preparedStatement = "INSERT INTO Book (Title, Author, PublicationYear, ISBN, InStock) " +
                "VALUES (@Title, @Author, @PublicationYear, @ISBN, @InStock)"; //ID is autoincremented

            MySqlCommand query = new(preparedStatement, conn);

            query .Parameters.AddWithValue("@Title", book.Title);
            query .Parameters.AddWithValue("@Author", book.Author);
            query .Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            query .Parameters.AddWithValue("@ISBN", book.ISBN);
            query .Parameters.AddWithValue("@InStock", book.InStock);

            await query.ExecuteNonQueryAsync();

            query.CommandText = "SELECT LAST_INSERT_ID()";
            book.ID = Convert.ToInt32(query.ExecuteScalar());

            return book;
        }

    }
}
