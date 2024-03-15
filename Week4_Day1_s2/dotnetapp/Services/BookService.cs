using System.Collections.Generic;
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

        public List<Book> GetBooks()
        {
            return _bookRepository.GetBooks();
        }

        public Book GetBook(int id)
        {
            return _bookRepository.GetBook(id);
        }

        public Book SaveBook(Book book)
        {
            return _bookRepository.SaveBook(book);
        }

        public Book UpdateBook(int id, Book book)
        {
            return _bookRepository.UpdateBook(id, book);
        }

        public bool DeleteBook(int id)
        {
            return _bookRepository.DeleteBook(id);
        }
    }
}
