
//public class BooksController : Controller
//{
//    private readonly IBookRepository _repo;
//    private readonly IGoogleBooksService _gb;

//    public BooksController(IBookRepository repo, IGoogleBooksService gb)
//    {
//        _repo = repo;
//        _gb = gb;
//    }

//    public IActionResult Index() => View(_repo.GetAll());

//    public IActionResult Details(int id)
//    {
//        var book = _repo.GetById(id);
//        return book is null ? NotFound() : View(book);
//    }

//    public IActionResult Create() => View();

//    [HttpPost, ValidateAntiForgeryToken]
//    public IActionResult Create(Book b)
//    {
//        if (!ModelState.IsValid) return View(b);
//        _repo.Add(b);
//        return RedirectToAction(nameof(Index));
//    }

//    public IActionResult Edit(int id)
//    {
//        var book = _repo.GetById(id);
//        return book is null ? NotFound() : View(book);
//    }

//    [HttpPost, ValidateAntiForgeryToken]
//    public IActionResult Edit(Book b)
//    {
//        if (!ModelState.IsValid) return View(b);
//        _repo.Update(b);
//        return RedirectToAction(nameof(Index));
//    }

//    public IActionResult Delete(int id)
//    {
//        var book = _repo.GetById(id);
//        return book is null ? NotFound() : View(book);
//    }

//    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
//    public IActionResult DeleteConfirmed(int id)
//    {
//        _repo.Delete(id);
//        return RedirectToAction(nameof(Index));
//    }

//    [HttpGet]
//    public IActionResult Search() => View();

//    [HttpPost, ValidateAntiForgeryToken]
//    public async Task<IActionResult> Search(string query)
//    {
//        ViewBag.Query = query;
//        var result = await _gb.SearchAsync(query);
//        return View(result);
//    }
//}
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookApp.Models;
using BookApp.Services;
using System.Linq;

namespace BookApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repo;
        private readonly IGoogleBooksService _gb;

        public BooksController(IBookRepository repo, IGoogleBooksService gb)
        {
            _repo = repo;
            _gb = gb;
        }

        // List all books
        [HttpGet]
        public IActionResult Index()
            => View(_repo.GetAll());

        // Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = _repo.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // Show empty form
        [HttpGet]
        public IActionResult Create()
            => View(new Book());

        // Handle form submit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Book model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _repo.Add(model);
            return RedirectToAction(nameof(Index));
        }

        // Show edit form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _repo.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // Handle edit submit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Book model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _repo.Update(model);
            return RedirectToAction(nameof(Index));
        }

        // Show delete confirmation
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _repo.GetById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // Handle delete
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Search form
        [HttpGet]
        public IActionResult Search()
            => View();

        // Search submit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchResults(string query)
        {
            ViewBag.Query = query;
            var result = await _gb.SearchAsync(query);
            return View("Results", result);
        }
        [HttpGet]
        public IActionResult Analytics()
        {
            var books = _repo.GetAll().ToList();
            return View(books);
        }

    }
}

//namespace BookApp.Controllers
//{
//    public class BooksController : Controller
//    {
//        private readonly IBookRepository _repo;
//        private readonly IGoogleBooksService _gb;

//        public BooksController(IBookRepository repo, IGoogleBooksService gb)
//        {
//            _repo = repo;
//            _gb = gb;
//        }

//        public IActionResult Index()
//        {
//            var books = _repo.GetAll();
//            return View(books);
//        }

//        public IActionResult Create() => View();

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Book model)
//        {
//            if (!ModelState.IsValid)
//            {
//                ModelState.Remove(nameof(model.Liked));
//                return View(model);
//            }
//            _repo.Add(model);
//            return RedirectToAction(nameof(Index));
//        }

//        public IActionResult Details(int id)
//        {
//            var book = _repo.GetById(id);
//            if (book == null) return NotFound();
//            return View(book);
//        }

//        public IActionResult Edit(int id)
//        {
//            var book = _repo.GetById(id);
//            if (book == null) return NotFound();
//            return View(book);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(Book model)
//        {
//            if (!ModelState.IsValid) return View(model);
//            _repo.Update(model);
//            return RedirectToAction(nameof(Index));
//        }

//        public IActionResult Delete(int id)
//        {
//            var book = _repo.GetById(id);
//            if (book == null) return NotFound();
//            return View(book);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            _repo.Delete(id);
//            return RedirectToAction(nameof(Index));
//        }

//        public IActionResult Search(string query) => View();

//        [HttpPost]
//        public async Task<IActionResult> SearchResults(string query)
//        {
//            var result = await _gb.SearchAsync(query);
//            return View("Results", result);
//        }
//    }
//}