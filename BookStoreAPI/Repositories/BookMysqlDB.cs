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
                var book = new Book()
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    PublicationYear = reader.GetInt32(3),
                    ISBN = reader.GetString(4),
                    InStock = reader.GetInt32(5),
                };
                bookList.Add(book);
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

            query.Parameters.AddWithValue("@Title", book.Title);
            query.Parameters.AddWithValue("@Author", book.Author);
            query.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            query.Parameters.AddWithValue("@ISBN", book.ISBN);
            query.Parameters.AddWithValue("@InStock", book.InStock);

            await query.ExecuteNonQueryAsync();

            query.CommandText = "SELECT LAST_INSERT_ID()";
            book.ID = Convert.ToInt32(query.ExecuteScalar());

            return book;
        }

        public async Task<Book?> GetIDAsync(int id)
        {
            await using MySqlConnection conn = new(_connectionString);
            conn.Open();

            var query = "SELECT ID, Title, Author, PublicationYear, ISBN, InStock FROM Book where ID = @ID";

            MySqlCommand preparedStatement = new(query, conn);
            preparedStatement.Parameters.AddWithValue("ID", id);

            await using var reader = await preparedStatement.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                return new Book()
                {
                    ID = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    PublicationYear = reader.GetInt32(3),
                    ISBN = reader.GetString(4),
                    InStock = reader.GetInt32(5),
                };
            }

            return null;
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            await using MySqlConnection conn = new(_connectionString);
            conn.Open();

            var preparedStatement = "UPDATE Book SET " +
                "Title=@Title, Author=@Author, PublicationYear=@PublicationYear, ISBN=@ISBN, InStock=@InStock  WHERE ID = @ID";

            MySqlCommand query = new(preparedStatement, conn);

            query.Parameters.AddWithValue("@Title", book.Title);
            query.Parameters.AddWithValue("@Author", book.Author);
            query.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            query.Parameters.AddWithValue("@ISBN", book.ISBN);
            query.Parameters.AddWithValue("@InStock", book.InStock);
            query.Parameters.AddWithValue("@ID", id); 

            var rowEffectedCount = await query.ExecuteNonQueryAsync();
            if (rowEffectedCount == 0)
                return null;

            return await GetIDAsync(id);

        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var bookToDelete = await GetIDAsync(id);

            await using MySqlConnection conn = new(_connectionString);
            conn.Open();

            var preparedStatement = "DELETE FROM Book Where ID = @id";

            MySqlCommand query = new(preparedStatement, conn);

            query.Parameters.AddWithValue("@ID", id);

            var rowEffectedCount = await query.ExecuteNonQueryAsync();

            if (rowEffectedCount > 0)
                return bookToDelete;

            return null;
        }
    }
}
