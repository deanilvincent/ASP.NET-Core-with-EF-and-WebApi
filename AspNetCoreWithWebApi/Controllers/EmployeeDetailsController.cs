using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWithWebApi.Models;

namespace AspNetCoreWithWebApi.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        private readonly MyDbContext _context;

        public EmployeeDetailsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeDetails.ToListAsync());
        }

        // GET: EmployeeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetails = await _context.EmployeeDetails
                .SingleOrDefaultAsync(m => m.EmpId == id);
            if (employeeDetails == null)
            {
                return NotFound();
            }

            return View(employeeDetails);
        }

        // GET: EmployeeDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,Firstname,Lastname,DateHired")] EmployeeDetails employeeDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDetails);
        }

        // GET: EmployeeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetails = await _context.EmployeeDetails.SingleOrDefaultAsync(m => m.EmpId == id);
            if (employeeDetails == null)
            {
                return NotFound();
            }
            return View(employeeDetails);
        }

        // POST: EmployeeDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,Firstname,Lastname,DateHired")] EmployeeDetails employeeDetails)
        {
            if (id != employeeDetails.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDetailsExists(employeeDetails.EmpId))
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
            return View(employeeDetails);
        }

        // GET: EmployeeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetails = await _context.EmployeeDetails
                .SingleOrDefaultAsync(m => m.EmpId == id);
            if (employeeDetails == null)
            {
                return NotFound();
            }

            return View(employeeDetails);
        }

        // POST: EmployeeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeDetails = await _context.EmployeeDetails.SingleOrDefaultAsync(m => m.EmpId == id);
            _context.EmployeeDetails.Remove(employeeDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDetailsExists(int id)
        {
            return _context.EmployeeDetails.Any(e => e.EmpId == id);
        }
    }
}
