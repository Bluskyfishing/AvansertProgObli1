﻿using BookStoreAPI.Models;
using BookStoreAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreAPI.Endpoints
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoints(this WebApplication app) // map different methods to an /url.
        {
            var BookGroup = app.MapGroup("/Books");

            BookGroup.MapGet("", GetBooksAsync).WithName("GetBooks");
            BookGroup.MapPost("", AddBookAsync).WithName("AddBook");
            BookGroup.MapPut("/{id}", UpdateBookAsync).WithName("UpdateBookAsync");
            BookGroup.MapDelete("/{id}", DeleteBookAsync).WithName("DeleteBookAsync");

        }

        private static async Task<IResult> GetBooksAsync([FromServices] IBookRepository repo, [FromQuery] int? id)
        {
            var Books = await repo.GetAllAsync();

            return id is null
                ? Results.Ok(Books)
                : Results.Ok(Books.Where(book => book.ID == id));
        }

        private static async Task<IResult> AddBookAsync(IBookRepository repo, Book book)
        {
            var p = await repo.AddAsync(book);
            return p is null
                ? Results.BadRequest("Failed to add database")
                : Results.Ok(p);
        }

        private static async Task<IResult> UpdateBookAsync(IBookRepository repo, int id, Book book)
        {
            var p = await repo.UpdateAsync(id, book);
            return p is null
                ? Results.BadRequest($"Failed to update book with id = '{id}'")
                : Results.Ok(p);
        }

        private static async Task<IResult> DeleteBookAsync(IBookRepository repo, int id)
        {
            var p = await repo.DeleteAsync(id);
            return p is null
                ? Results.BadRequest($"Can't find book with id = '{id}'")
                : Results.Ok(p);
        }
    }
}
