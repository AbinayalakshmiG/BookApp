// File: Services/GoogleBooksResponse.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace BookApp.Services;

public class GoogleBooksResponse
{
    public int TotalItems { get; set; }
    public BookItem[] Items { get; set; } = Array.Empty<BookItem>();
}

public class BookItem
{
    public VolumeInfo VolumeInfo { get; set; } = new();
}

public class VolumeInfo
{
    public string Title { get; set; } = string.Empty;
    public string[] Authors { get; set; } = Array.Empty<string>();
    public string? Description { get; set; }
    public ImageLinks? ImageLinks { get; set; }
}

public class ImageLinks
{
    public string? Thumbnail { get; set; }
}
