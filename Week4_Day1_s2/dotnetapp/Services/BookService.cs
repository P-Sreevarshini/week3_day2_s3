using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
using dotnetapp.Repository;

namespace dotnetapp.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            _bookRepository.SaveBook(book);
        }

        public void UpdateBook(int id, Book book)
        {
            _bookRepository.UpdateBook(id, book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.DeleteBook(id);
        }
    }
}
