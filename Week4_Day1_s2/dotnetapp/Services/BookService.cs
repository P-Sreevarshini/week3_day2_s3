using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>
            {
                new Book { BookId = 1, BookName = "Book 1", Category = "Fiction", Price = 20.99m },
                new Book { BookId = 2, BookName = "Book 2", Category = "Non-Fiction", Price = 15.99m }
            };
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.BookId == id);
        }

        public void AddBook(Book book)
        {
            book.BookId = _books.Max(b => b.BookId) + 1;
            _books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.BookId == book.BookId);
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
