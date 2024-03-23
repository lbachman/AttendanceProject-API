using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI_v3.AttendanceModels;

namespace AttendanceAPI_v3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        private readonly AttendanceContext _context;

        public CommunicationsController(AttendanceContext context)
        {
            _context = context;
        }



        // GET: api/Communications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Communication>>> GetCommunications()
        {
            return await _context.Communications.ToListAsync();
        }



        // GET: api/Communications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Communication>> GetCommunication(uint id)
        {
            var communication = await _context.Communications.FindAsync(id);

            if (communication == null)
            {
                return NotFound();
            }

            return communication;
        }



        // PUT: api/Communications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunication(uint id, Communication communication)
        {
            if (id != communication.ComId)
            {
                return BadRequest();
            }

            _context.Entry(communication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunicationExists(id))
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



        // POST: api/Communications
        [HttpPost]
        public async Task<ActionResult<Communication>> PostCommunication(Communication communication)
        {
            _context.Communications.Add(communication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommunication", new { id = communication.ComId }, communication);
        }



        // DELETE: api/Communications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunication(uint id)
        {
            var communication = await _context.Communications.FindAsync(id);
            if (communication == null)
            {
                return NotFound();
            }

            _context.Communications.Remove(communication);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool CommunicationExists(uint id)
        {
            return _context.Communications.Any(e => e.ComId == id);
        }
    }
}
