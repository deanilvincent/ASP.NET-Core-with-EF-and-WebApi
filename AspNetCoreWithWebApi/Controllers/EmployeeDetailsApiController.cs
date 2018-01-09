using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWithWebApi.Models;

namespace AspNetCoreWithWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/EmployeeDetailsApi")]
    public class EmployeeDetailsApiController : Controller
    {
        private readonly MyDbContext _context;

        public EmployeeDetailsApiController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeDetailsApi
        [HttpGet]
        public IEnumerable<EmployeeDetails> GetEmployeeDetails()
        {
            return _context.EmployeeDetails;
        }

        // GET: api/EmployeeDetailsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeDetails = await _context.EmployeeDetails.SingleOrDefaultAsync(m => m.EmpId == id);

            if (employeeDetails == null)
            {
                return NotFound();
            }

            return Ok(employeeDetails);
        }

        // PUT: api/EmployeeDetailsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDetails([FromRoute] int id, [FromBody] EmployeeDetails employeeDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeDetails.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(employeeDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeDetailsApi
        [HttpPost]
        public async Task<IActionResult> PostEmployeeDetails([FromBody] EmployeeDetails employeeDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmployeeDetails.Add(employeeDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeDetails", new { id = employeeDetails.EmpId }, employeeDetails);
        }

        // DELETE: api/EmployeeDetailsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeDetails = await _context.EmployeeDetails.SingleOrDefaultAsync(m => m.EmpId == id);
            if (employeeDetails == null)
            {
                return NotFound();
            }

            _context.EmployeeDetails.Remove(employeeDetails);
            await _context.SaveChangesAsync();

            return Ok(employeeDetails);
        }

        private bool EmployeeDetailsExists(int id)
        {
            return _context.EmployeeDetails.Any(e => e.EmpId == id);
        }
    }
}