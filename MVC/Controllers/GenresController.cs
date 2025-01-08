using Microsoft.AspNetCore.Mvc;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;

namespace MVC.Controllers
{
    public class GenresController : MvcController
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: Genres
        public IActionResult Index()
        {
            var list = _genreService.Query().ToList();
            return View(list);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            var item = _genreService.Query().SingleOrDefault(g => g.Record.Id == id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                var result = _genreService.Create(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public IActionResult Edit(int id)
        {
            var item = _genreService.Query().SingleOrDefault(g => g.Record.Id == id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        // POST: Genres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                var result = _genreService.Update(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public IActionResult Delete(int id)
        {
            var item = _genreService.Query().SingleOrDefault(g => g.Record.Id == id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _genreService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
} 