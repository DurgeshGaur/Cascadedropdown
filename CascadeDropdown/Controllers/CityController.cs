using CascadeDropdown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CascadeDropdown.Controllers
{
    public class CityController : Controller
    {
        private readonly ApplicationDbCOntext _context;
        public CityController(ApplicationDbCOntext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.City.Include(x => x.Country).Include(x => x.State).ToListAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Country = new SelectList(await _context.Country.ToListAsync(), "Id", "Name");
             
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(City country)
        {
            var model = new City 
            { 
                CountryId = country.CountryId, 
                StateId =country.StateId, 
                Name = country.Name 
            };
            await _context.City.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var model = await _context.City.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ViewBag.Country = new SelectList(await _context.Country.ToListAsync(), "Id", "Name");
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(City Country)
        {
            var model = await _context.City.Where(x => x.Id == Country.Id).FirstOrDefaultAsync();
            if (model == null)
                return View(Country);
            model.CountryId = Country.CountryId;
            model.StateId = Country.StateId;
            model.Name = Country.Name;
            _context.City.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var model = await _context.City.Where(x => x.Id == Id).FirstOrDefaultAsync();
            _context.City.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
        public async Task<IActionResult> GetState(int countryId)
        {
            var staet = await _context.State.Where(x => x.CountryId == countryId).ToListAsync();
            return Json(staet);
        }
    }
}
