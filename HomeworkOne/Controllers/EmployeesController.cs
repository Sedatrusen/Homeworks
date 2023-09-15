using HomeworkOne.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkOne.Controllers
{
    public class EmployeesController : Controller

        
    {
        private readonly NortwindContext _nortwindContext;

        public EmployeesController (NortwindContext nortwindContext)
        {
            _nortwindContext= nortwindContext;


        }
        public async Task<IActionResult> Index()
        {
            return View(await _nortwindContext.Employees.ToListAsync());
        }
        public async Task<IActionResult> Orders(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }
            var orders = await _nortwindContext.Orders.ToListAsync();

            orders= orders.FindAll(o => o.EmployeeId == id);


            return View(orders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Title")] Employees employees)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                  await  _nortwindContext.Employees.AddAsync(employees);
                    await _nortwindContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employees);
        }

        private bool EmployeesExists(object ıd)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _nortwindContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }
    }
}
