using CascadeDropdown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CascadeDropdown.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbCOntext _context;
        public EmployeeController(ApplicationDbCOntext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Employees.Include(x => x.Country).Include(x => x.State).Include(x => x.City).ToListAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Country = new SelectList(await _context.Country.ToListAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee country)
        {
            var model = new Employee 
            { 
                Name = country.Name, 
                DateTime = country.DateTime, 
                Email = country.Email,
                CountryId=country.CountryId, 
                StateId = country.StateId,
                CityId = country.CityId 
            };
            await _context.Employees.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var model = await _context.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();
            ViewBag.Country = new SelectList(await _context.Country.ToListAsync(), "Id", "Name");
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee Country)
        {
            var model = await _context.Employees.Where(x => x.Id == Country.Id).FirstOrDefaultAsync();
            if (model == null)
                return View(Country);

            model.Name = Country.Name;
            model.Email = Country.Email;
            model.DateTime = Country.DateTime;
            model.CountryId = Country.CountryId;
            model.StateId = Country.StateId;
            model.CityId = Country.CityId;
            _context.Employees.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var model = await _context.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();
            _context.Employees.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public JsonResult GetCity(int stateId)
        {
            var cities = _context.City
                .Where(c => c.StateId == stateId)
                .Select(c => new { id = c.Id, name = c.Name })
                .ToList();

            return Json(cities);
        }
    }
}
