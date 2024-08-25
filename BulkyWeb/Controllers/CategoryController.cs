using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Category>? category = _categoryRepository.GetAll().ToList();
            return View(category);
        }


        //public IActionResult Edit()
        //{
        //    //List<Category>? category = _categoryRepository.Categories.ToList();
        //    //return View(category);
        //}


        public IActionResult Create()
        {
            //List<Category>? category = _dbContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category name can-not be same as Display order");

            }

            if (obj.Name!= null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is not allowed");
            }

            if (ModelState.IsValid)
            {
                _categoryRepository.Add(obj);
                _categoryRepository.Save();
                return RedirectToAction("Index");
            }   return View();
        }
    }
}
