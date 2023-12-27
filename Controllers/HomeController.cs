using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignIn.Models;
using Microsoft.AspNetCore.Http;
namespace SignIn.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInContext context;

        public HomeController(SignInContext context)
        {
            this.context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return View(await context.EmployeeTables.ToListAsync());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTable = await context.EmployeeTables
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeeTable == null)
            {
                return NotFound();
            }

            return View(employeeTable);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
       
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("Username").ToString();
            }
            else
                RedirectToAction("Login");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Register(EmployeeTable emp)
        {
            if (ModelState.IsValid)
            {
                await context.EmployeeTables.AddAsync(emp);
                await context.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully";
               return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(EmployeeTable emp)
        {
            var NewEmp = context.EmployeeTables.Where(x => x.Username == emp.Username && x.Password == emp.Password).FirstOrDefault();
            if (NewEmp != null)
            {
                HttpContext.Session.SetString("Username", emp.Username);
                return RedirectToAction("Dashboard");
            }
            else
                ViewBag.Message = "Login Failed......";
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                HttpContext.Session.Remove("Username");
                RedirectToAction("Login");
            }

            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,CompId,Username,Password")] EmployeeTable employeeTable)
        {
            if (ModelState.IsValid)
            {
                context.Add(employeeTable);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeTable);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTable = await context.EmployeeTables.FindAsync(id);
            if (employeeTable == null)
            {
                return NotFound();
            }
            return View(employeeTable);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,CompId,Username,Password")] EmployeeTable employeeTable)
        {
            if (id != employeeTable.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(employeeTable);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTableExists(employeeTable.EmpId))
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
            return View(employeeTable);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeTable = await context.EmployeeTables
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeeTable == null)
            {
                return NotFound();
            }

            return View(employeeTable);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeTable = await context.EmployeeTables.FindAsync(id);
            if (employeeTable != null)
            {
                context.EmployeeTables.Remove(employeeTable);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeTableExists(int id)
        {
            return context.EmployeeTables.Any(e => e.EmpId == id);
        }
    }
}
