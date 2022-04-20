using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RevClothing.Data;
using RevClothing.Models;

namespace RevClothing.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Items.Include(p => p.ItemId).ToList();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item items)
        {
            var selectedCategory = _context.Items.Where(c => c.ItemId == items.ItemId).SingleOrDefault();
            var item = new Item();

            item.Name = items.Name;
            item.code = items.code;
            item.Description = items.Description;
            item.Price = items.Price;
            item.DateAdded = DateTime.Now;
            item.DateModified = DateTime.Now;
            item.Type = items.Type;
            item.ItemId = items.ItemId;

            _context.Items.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var item = _context.Items.Where(p => p.ItemId == id).SingleOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Item items)
        {
            var item = _context.Items.Where(p => p.ItemId == id).SingleOrDefault();

            var selectedCategory = _context.Items.Where(c => c.ItemId == items.ItemId).SingleOrDefault();

            item.Name = items.Name;
            item.code = items.code;
            item.Description = items.Description;
            item.Price = items.Price;
            item.DateAdded = DateTime.Now;
            item.DateModified = DateTime.Now;
            item.Type = items.Type;
            item.ItemId = items.ItemId;

            _context.Items.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }

            var item = _context.Items.Where(p => p.ItemId == id).SingleOrDefault();
            if(item == null)
            {
                return RedirectToAction("Index");
            }

            _context.Items.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
