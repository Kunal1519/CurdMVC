using Microsoft.AspNetCore.Mvc;
using MvcDemoCurd.DataAccess;
using MvcDemoCurd.Models;

namespace MvcDemo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _db.Categories;
            return View(ObjCategoryList);
        }
        //GET Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
             //   ModelState.AddModelError("CustomError", "They can't be same");
                ModelState.AddModelError("name", "They can't be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Data is added SuccessFully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET Method
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFormDb = _db.Categories.Find(id);
            //var categoryFormDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if(categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);
        }

        //POST Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //   ModelState.AddModelError("CustomError", "They can't be same");
                ModelState.AddModelError("name", "They can't be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Data is Updated SuccessFully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET Method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFormDb = _db.Categories.Find(id);
            //var categoryFormDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFormDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFormDb == null)
            {
                return NotFound();
            }
            return View(categoryFormDb);
        }

        //POST Method
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Data is Deleted SuccessFully";
            return RedirectToAction("Index");
        }
    }
}
