using System.ComponentModel.DataAnnotations;

namespace BookApp.Models;

//public class Book
//{
//    public int Id { get; set; }

//    [Required, StringLength(200)]
//    public string Title { get; set; } = "";

//    [Required, StringLength(100)]
//    public string Author { get; set; } = "";

//    [StringLength(50)]
//    public string Genre { get; set; } = "";

//    [Range(1, 5)]
//    public int Rating { get; set; }

//    [DataType(DataType.MultilineText)]
//    public string Review { get; set; } = "";
//}
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int Rating { get; set; }          // 1–5
    public string Review { get; set; } = string.Empty;
    public bool Liked { get; set; }
}