using System;
using System.Threading.Tasks;

namespace BookApp.Services;

public interface IGoogleBooksService
{
    Task<GoogleBooksResponse?> SearchAsync(string query);
}

//public class GoogleBooksResponse
//{
//    public class VolumeInfo
//    {
//        public string Title { get; set; } = "";
//        public string[] Authors { get; set; } = Array.Empty<string>();
//        public string? Description { get; set; }
//    }

//    public class Item
//    {
//        public string Id { get; set; } = "";
//        public VolumeInfo VolumeInfo { get; set; } = new();
//    }

//    public Item[] Items { get; set; } = Array.Empty<Item>();
//}