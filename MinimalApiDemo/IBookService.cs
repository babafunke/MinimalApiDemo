namespace MinimalApiDemo;

public interface IBookService
{
    List<Book> GetAll();
    Book? GetById(int id);
    void Add(Book book);
    Book? Delete(int id);
    Book? Update(int id, Book updatedBook);
}