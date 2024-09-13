using BookStoreAPI.Models;
using System;

namespace BookStoreAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetAllAsync();

    }   
}
