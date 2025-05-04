using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookRepository _repo;
        private readonly IGoogleBooksService _gb;

        public BooksController(IBookRepository repo, IGoogleBooksService gb)
        {
            _repo = repo;
            _gb = gb;
        }

        // GET: /Books
        [HttpGet]
        public IActionResult Index(string sortOrder, string filter)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var myBooks = _repo.GetAll()
                               .Where(b => b.OwnerId == userId);

            //filtering
            filter = filter ?? "all";
            ViewBag.CurrentFilter = filter;
            myBooks = filter switch
            {
                "liked" => myBooks.Where(b => b.Liked),
                "notliked" => myBooks.Where(b => !b.Liked),
                _ => myBooks  // "all" or any other => no filtering
            };

            // sorting
            myBooks = sortOrder switch
            {
                "title_desc" => myBooks.OrderByDescending(b => b.Title),
                "author_asc" => myBooks.OrderBy(b => b.Author),
                "author_desc" => myBooks.OrderByDescending(b => b.Author),
                "rating_asc" => myBooks.OrderBy(b => b.Rating),
                "rating_desc" => myBooks.OrderByDescending(b => b.Rating),
                _ => myBooks.OrderBy(b => b.Title) // title_asc default
            };

            ViewBag.CurrentSort = sortOrder ?? "title_asc";
            return View(myBooks);
        }

        // GET: /Books/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = _repo.GetById(id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (book == null || book.OwnerId != userId)
                return NotFound();
            return View(book);
        }

        // GET: /Books/Create
        [HttpGet]
        public IActionResult Create()
            => View(new Book());

        // POST: /Books/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Book model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Stamp with the current user’s Id
            model.OwnerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _repo.Add(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Books/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _repo.GetById(id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (book == null || book.OwnerId != userId)
                return NotFound();
            return View(book);
        }

        // POST: /Books/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Book model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Preserve owner and update
            model.OwnerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _repo.Update(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Books/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _repo.GetById(id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (book == null || book.OwnerId != userId)
                return NotFound();
            return View(book);
        }

        // POST: /Books/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _repo.GetById(id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (book == null || book.OwnerId != userId)
                return NotFound();

            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Books/Search
        [HttpGet]
        public IActionResult Search()
        {
            ViewBag.Query = "";
            return View();
        }

        // POST: /Books/SearchResults
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchResults(string query)
        {
            ViewBag.Query = query;

            if (string.IsNullOrWhiteSpace(query))
                return View("Search");

            var result = await _gb.SearchAsync(query);
            return View(result);
        }

        // GET: /Books/Analytics
        [HttpGet]
        public IActionResult Analytics()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var myBooks = _repo.GetAll()
                               .Where(b => b.OwnerId == userId)
                               .ToList();
            return View(myBooks);
        }

        // GET: /Books/About
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
