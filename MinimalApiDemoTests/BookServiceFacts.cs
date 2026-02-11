using MinimalApiDemo;

namespace MinimalApiDemoTests;

public class BookServiceFacts
{
    private readonly BookService sut = new();

    [Fact]
    public void GetAllShouldReturnAllBooks()
    {
        var response = sut.GetAll();

        Assert.Equal(3, response.Count);
    }

    [Fact]
    public void GetAllShouldReturnType()
    {
        var response = sut.GetAll();

        Assert.IsType<List<Book>>(response);
    }

    [Fact]
    public void GetByIdShouldReturnType()
    {
        var response = sut.GetById(2);

        Assert.IsType<Book>(response);
    }
}