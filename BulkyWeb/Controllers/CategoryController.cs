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
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category>? category = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }   return View();
        }


        public IActionResult Delete(int? Id)
        {
            if(Id == null )
            {
                return NotFound();
            }

           Category category= _unitOfWork.Category.Get(c => c.Id == Id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            if (obj == null)
                return NotFound();

            _unitOfWork.Category.Remove(obj);

            _unitOfWork.Save();
            List<Category>? category = _unitOfWork.Category.GetAll().ToList();
            return RedirectToAction("Index");
        }




        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Category category = _unitOfWork.Category.Get(c => c.Id == Id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj == null)
                return NotFound();

            _unitOfWork.Category.Update(obj);

            _unitOfWork.Save();
            List<Category>? category = _unitOfWork.Category.GetAll().ToList();
            return RedirectToAction("Index");
        }
    }
}
