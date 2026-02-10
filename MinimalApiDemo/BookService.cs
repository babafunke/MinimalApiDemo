namespace MinimalApiDemo;

public class BookService : IBookService
{
    private readonly List<Book> _books =
    [
        new() { Id = 1, Title = "The Great Gatsby" },
        new() { Id = 2, Title = "To Kill a Mockingbird" },
        new() { Id = 3, Title = "1984" },
        new() { Id = 4, Title = "Pride and Prejudice" }
    ];

    public List<Book> GetAll() => _books;

    public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public void Add(Book book) => _books.Add(book);

    public Book? Delete(int id)
    {
        var book = GetById(id);
        if (book is not null) _books.Remove(book);
        return book;
    }

    public Book? Update(int id, Book updatedBook)
    {
        var book = GetById(id);
        if (book is not null)
        {
            book.Title = updatedBook.Title;
        }
        return book;
    }
}