// File: Services/IUserService.cs
using BookApp.Models;
using System.Collections.Generic;

namespace BookApp.Services;
public interface IUserService
{
    User? Validate(string username, string password);
    User? GetById(int id);
    IEnumerable<User> GetAll();
}
