using BookStoreAPI.Models;
using System;

namespace BookStoreAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetAllAsync(); //Get every book
        Task<Book?> AddAsync(Book book); //Add an book

        //Task<Book?> UpdateAsync(int id, Book book);
    }   
}
