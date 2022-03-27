using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        // ApplicationDbContext comes from Dependancy Injection via the instructor
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Strongly typed using Cateogy Model
            IEnumerable<Category> categoryList = _db.Categories.ToList();

            // Passing cateogyList in the View
            return View(categoryList);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST
        /// Uses ValidateAntiForgeryToeken - Prevents cross attack, injects a key into any of your forms that can later be validated.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Category catObj)
        {
            if (catObj.Name == catObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Display Order and Name cannot match.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(catObj);
                _db.SaveChanges();
                //Tempdata is only there once, gone after a refresh
                TempData["success"] = "Category created successfully";

                // TODO - Replace magic strings
                return RedirectToAction("Index");
            }
            return View(catObj);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            return View(catFromDb);
        }

        /// <summary>
        /// POST
        /// Uses ValidateAntiForgeryToeken - Prevents cross attack, injects a key into any of your forms that can later be validated.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category catObj)
        {
            if (catObj.Name == catObj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Display Order and Name cannot match.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(catObj);
                _db.SaveChanges();

                //Tempdata is only there once, gone after a refresh
                TempData["success"] = "Category updated successfully";

                // TODO - Replace magic strings
                return RedirectToAction("Index");
            }
            return View(catObj);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            return View(catFromDb);
        }


        /// <summary>
        /// POST
        /// Uses ValidateAntiForgeryToeken - Prevents cross attack, injects a key into any of your forms that can later be validated.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category catObj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(catObj);
                _db.SaveChanges();

                //Tempdata is only there once, gone after a refresh
                TempData["success"] = "Category deleted successfully";

                // TODO - Replace magic strings
                return RedirectToAction("Index");
            }
            return View(catObj);
        }
    }
}
