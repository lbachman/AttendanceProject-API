using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AttendanceProjectAPI_v2.Models;
namespace AttendanceEntityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendsController : ControllerBase
    {
        private readonly AttendanceContext _context;

        public AttendsController(AttendanceContext context)
        {
            _context = context;
        }



        [Authorize]

        // GET: api/Attends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attend>>> GetAttends()
        {
            return await _context.Attends.ToListAsync();
        }



        // GET: api/Attends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attend>> GetAttend(string id)
        {
            var attend = await _context.Attends.FindAsync(id);

            if (attend == null)
            {
                return NotFound();
            }

            return attend;
        }

        // PUT: api/Attends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttend(string id, Attend attend)
        {
            if (id != attend.StudentUuid)
            {
                return BadRequest();
            }

            _context.Entry(attend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendExists(id))
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

        // POST: api/Attends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attend>> PostAttend(Attend attend)
        {
            _context.Attends.Add(attend);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AttendExists(attend.StudentUuid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAttend", new { id = attend.StudentUuid }, attend);
        }

        // DELETE: api/Attends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttend(string id)
        {
            var attend = await _context.Attends.FindAsync(id);
            if (attend == null)
            {
                return NotFound();
            }

            _context.Attends.Remove(attend);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendExists(string id)
        {
            return _context.Attends.Any(e => e.StudentUuid == id);
        }
    }
}
