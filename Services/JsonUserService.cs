// File: Services/JsonUserService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;                   
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BookApp.Models;
using Microsoft.AspNetCore.Hosting;

namespace BookApp.Services;
public class JsonUserService : IUserService
{
    private readonly string _filePath;
    private readonly List<User> _users;

    public JsonUserService(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "users.json");
        var json = File.ReadAllText(_filePath);
        _users = JsonSerializer.Deserialize<List<User>>(json)
                 ?? new List<User>();
    }

    public IEnumerable<User> GetAll() => _users;

    public User? GetById(int id) =>
        _users.SingleOrDefault(u => u.Id == id);

    public User? Validate(string username, string password)
    {
        Console.WriteLine($"Attempting login for '{username}'");
        foreach (var u in _users)
            Console.WriteLine($"  Loaded user: {u.Username}");

        var user = _users.SingleOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (user == null) return null;

        // compare SHA256 hash
        using var sha = SHA256.Create();
        var hash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));

        Console.WriteLine($" → Computed hash: {hash}");
        Console.WriteLine($" → Stored hash:   {user.PasswordHash}");

        return hash == user.PasswordHash ? user : null;
    }
}
