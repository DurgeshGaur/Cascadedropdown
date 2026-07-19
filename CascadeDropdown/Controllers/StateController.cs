using CascadeDropdown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CascadeDropdown.Controllers
{
    public class StateController : Controller
    {
        private readonly ApplicationDbCOntext _context;
        public StateController(ApplicationDbCOntext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.State.Include(x => x.Country).ToListAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Country = new SelectList(await _context.Country.ToListAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(State country)
        {
            var model = new State { CountryId = country.CountryId, Name = country.Name };
            await _context.State.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var model = await _context.State.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ViewBag.Country = new SelectList(await _context.Country.ToListAsync(), "Id", "Name");
            
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(State Country)
        {
            var model = await _context.State.Where(x => x.Id == Country.Id).FirstOrDefaultAsync();
            if (model == null)
                return View(Country);
            model.CountryId = Country.CountryId;
            model.Name = Country.Name;
            _context.State.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var model = await _context.State.Where(x => x.Id == Id).FirstOrDefaultAsync();
            _context.State.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
