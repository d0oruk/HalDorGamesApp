﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;
using BLL.Services;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class GamesController : MvcController
    {
        // Service injections:
        private readonly IGameService _gameService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public GamesController(
			IGameService gameService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _gameService = gameService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Games
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _gameService.Query().ToList();
            return View(list);
        }

        // GET: Games/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _gameService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameModel game)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _gameService.Create(game.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = game.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(game);
        }

        // GET: Games/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _gameService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Games/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameModel game)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _gameService.Update(game.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = game.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(game);
        }

        // GET: Games/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _gameService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Games/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _gameService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
