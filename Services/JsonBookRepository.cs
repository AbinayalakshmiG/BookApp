// File: Services/JsonBookRepository.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BookApp.Models;
using Microsoft.AspNetCore.Hosting;

namespace BookApp.Services;

public class JsonBookRepository : IBookRepository
{
    private static readonly string FileName = "books.json";
    private readonly string _filePath;
    private List<Book> _items;

    public JsonBookRepository(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, FileName);
        if (File.Exists(_filePath))
            _items = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText(_filePath))
                     ?? new List<Book>();
        else
            _items = new List<Book>();
    }

    public static void InitializeDataFile(string path)
    {
        var file = Path.Combine(path, FileName);
        if (!File.Exists(file))
            File.WriteAllText(file, JsonSerializer.Serialize(new List<Book>()));
    }

    public IEnumerable<Book> GetAll() => _items;
    public Book? GetById(int id) => _items.FirstOrDefault(b => b.Id == id);

    public void Add(Book book)
    {
        book.Id = _items.Any() ? _items.Max(b => b.Id) + 1 : 1;
        _items.Add(book);
        Save();
    }

    public void Update(Book book)
    {
        var existing = GetById(book.Id);
        if (existing is null) return;
        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Genre = book.Genre;
        existing.Rating = book.Rating;
        existing.Review = book.Review;
        existing.Liked = book.Liked;

        Save();
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null) return;
        _items.Remove(existing);
        Save();
    }

    private void Save()
    {
        File.WriteAllText(_filePath, JsonSerializer.Serialize(_items));
    }
}
