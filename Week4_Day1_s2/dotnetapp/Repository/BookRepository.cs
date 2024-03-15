using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Repository
{
    public class BookRepository
    {
        private static List<Book> _books = new List<Book>()
        {
            new Book { BookId = 1, BookName = "Book 1", Category = "Fiction", Price = 10.99m },
            new Book { BookId = 2, BookName = "Book 2", Category = "Non-Fiction", Price = 12.99m },
            new Book { BookId = 3, BookName = "Book 3", Category = "Science", Price = 15.99m }
        };

        public List<Book> GetBooks() => _books;

        public Book GetBook(int id) => _books.FirstOrDefault(b => b.BookId == id);

        public void SaveBook(Book book)
        {
            book.BookId = _books.Count + 1;
            _books.Add(book);
        }

        public void UpdateBook(int id, Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.BookId == id);
            if (existingBook != null)
            {
                existingBook.BookName = book.BookName;
                existingBook.Category = book.Category;
                existingBook.Price = book.Price;
            }
        }

        public void DeleteBook(int id)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.BookId == id);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
            }
        }
    }
}
