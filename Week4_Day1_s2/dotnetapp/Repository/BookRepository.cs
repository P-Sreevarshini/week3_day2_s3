using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Repository
{
    public class BookRepository
    {
        private readonly List<Book> _books = new List<Book>();

        public List<Book> GetAllBooks() => _books;

        public Book GetBookById(int id) => _books.FirstOrDefault(b => b.BookId == id);

        public void AddBook(Book book)
        {
            // Generate a new BookId
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
