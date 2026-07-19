using CascadeDropdown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CascadeDropdown.Controllers
{
    public class CountryController : Controller
    {
        private readonly ApplicationDbCOntext _context;
        public CountryController(ApplicationDbCOntext context)
        {
              _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Country.ToListAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Country country)
        {
            var model = new Country { Id = country.Id, Name = country.Name };
            await _context.Country.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult>Edit(int Id)
        {
            var model = await _context.Country.Where(x => x.Id == Id).FirstOrDefaultAsync();
                return View(model);
             
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Country Country)
        {
            var model = await _context.Country.Where(x => x.Id == Country.Id).FirstOrDefaultAsync();
            if (model == null)
                return View(Country);
            
            model.Name = Country.Name;
             _context.Country.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult>Delete(int Id)
        {
            var model = await _context.Country.Where(x => x.Id == Id).FirstOrDefaultAsync();
             _context.Country.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
