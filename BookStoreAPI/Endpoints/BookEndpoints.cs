using BookStoreAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreAPI.Endpoints
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoints(this WebApplication app)
        {
            var BookGroup = app.MapGroup("/Books");

            BookGroup.MapGet("", GetBooksAsync).WithName("GetBooks");

        }

        private static async Task<IResult> GetBooksAsync([FromServices] IBookRepository repo, [FromQuery] int? id)
        {
            var Books = await repo.GetAllAsync();
            // hente fra databasen !!
            return id is null
                ? Results.Ok(Books)
                : Results.Ok(Books.Where(book => book.ID == id));
        }

    }
}
