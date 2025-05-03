using BookApp.Models;
using System.Collections.Generic;

namespace BookApp.Services;

//public interface IBookRepository
//{
//    IEnumerable<Book> GetAll();
//    Book? GetById(int id);
//    void Add(Book book);
//    void Update(Book book);
//    void Delete(int id);
//}
public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    void Add(Book book);
    void Update(Book book);
    void Delete(int id);
}
