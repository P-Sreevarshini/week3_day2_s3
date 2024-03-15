
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Repository
{
public class BookRepository
{
    private readonly List<Book> _books = new List<Book>();

    public List<Book> GetBooks() => _books;

    public Book GetBook(int id) => _books.FirstOrDefault(b => b.BookId == id);

    public Book SaveBook(Book book)
    {
        book.BookId = _books.Count + 1;
        _books.Add(book);
        return book;
    }

    public Book UpdateBook(int id, Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.BookId == id);
        if (existingBook != null)
        {
            existingBook.BookName = book.BookName;
            existingBook.Category = book.Category;
            existingBook.Price = book.Price;
        }
        return existingBook;
    }

    public bool DeleteBook(int id)
    {
        var bookToRemove = _books.FirstOrDefault(b => b.BookId == id);
        if (bookToRemove != null)
        {
            _books.Remove(bookToRemove);
            return true;
        }
        return false;
    }
}
}