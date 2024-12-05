#nullable disable
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
    public class DevelopersController : MvcController
    {
        // Service injections:
        private readonly IDevelopersService _developerService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public DevelopersController(
			IDevelopersService developerService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _developerService = developerService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Developers
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _developerService.Query().ToList();
            return View(list);
        }

        // GET: Developers/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _developerService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Developers/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Developers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeveloperModel developer)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _developerService.Create(developer.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = developer.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(developer);
        }

        // GET: Developers/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _developerService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Developers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DeveloperModel developer)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _developerService.Update(developer.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = developer.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(developer);
        }

        // GET: Developers/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _developerService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Developers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _developerService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
