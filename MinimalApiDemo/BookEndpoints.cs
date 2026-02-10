// Plan (pseudocode):
// - Define an IBookService interface that encapsulates CRUD operations.
// - Provide an InMemoryBookService implementing IBookService (keeps initial seed data).
// - Provide an extension method AddBookServices(this IServiceCollection) to register the default implementation.
// - Provide MapBookEndpoints(this WebApplication) that maps endpoints.
//   - Each endpoint takes IBookService as a parameter so DI can inject a test double in tests.
//   - Preserve original behavior and responses (Ok, NotFound, Created).
// - This makes behavior injectable/testable: tests can register a mock IBookService and call MapBookEndpoints on a test WebApplication.

// Minimal, DI-friendly endpoint mapping and in-memory implementation.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace MinimalApiDemo;


public static class BookEndpoints
{
    public static WebApplication MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (IBookService svc) => svc.GetAll())
           .WithName("GetBooks")
           .WithOpenApi();

        app.MapGet("/books/{id}", (int id, IBookService svc) =>
        {
            var book = svc.GetById(id);
            return book is not null ? Results.Ok(book) : Results.NotFound($"Book with Id {id} does not exist.");
        });

        app.MapPost("/books", (Book book, IBookService svc) =>
        {
            svc.Add(book);
            return Results.Created($"/books/{book.Id}", book);
        });

        app.MapDelete("/books/{id}", (int id, IBookService svc) =>
        {
            var book = svc.Delete(id);
            return book is not null ? Results.Ok(book) : Results.NotFound($"Book with Id {id} does not exist.");
        });

        app.MapPut("/books/{id}", (int id, Book updatedBook, IBookService svc) =>
        {
            var book = svc.Update(id, updatedBook);
            return book is not null ? Results.Ok(book) : Results.NotFound($"Book with Id {id} does not exist.");
        });

        return app;
    }
}