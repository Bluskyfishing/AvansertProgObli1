using BookStoreAPI.Models;
using System;

namespace BookStoreAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetAllAsync(); // Gets every book 
        Task<Book?> AddAsync(Book book); // Adds a book
        Task<Book?> UpdateAsync(int id, Book book); // Updates a book
        Task<Book?> DeleteAsync(int id); // Deletes a book

    }
}
