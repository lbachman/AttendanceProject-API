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
    public class AttendsController : ControllerBase
    {
        private readonly AttendanceContext _context;

        public AttendsController(AttendanceContext context)
        {
            _context = context;
        }


        /// <summary>
        /// get all attendance records
        /// </summary>
        /// <returns></returns>
        // GET: api/Attends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attend>>> GetAttends()
        {
            return await _context.Attends.ToListAsync();
        }



        /// <summary>
        /// get attendance by student id
        /// </summary>
        /// <param name="studentUuid"></param>
        /// <returns></returns>
        // GET: api/Attends/ByStudent/{studentUuid}
        [HttpGet("by-id/{studentUuid}")]
        public async Task<ActionResult<IEnumerable<Attend>>> GetStudentAttendance(string studentUuid)
        {
            var attends = await _context.Attends
                .Where(a => a.StudentUuid == studentUuid)
                .ToListAsync();

            if (attends == null)
            {
                return NotFound();
            }

            return attends;
        }



        /// <summary>
        /// gets all attendance records for a class by class id
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        // GET: api/Attends/ByStudent/{studentUuid}
        [HttpGet("class-id/{classId}")]
        public async Task<ActionResult<IEnumerable<Attend>>> GetClassAttendance(uint classId)
        {
            var attends = await _context.Attends
                .Where(a => a.ClassId == classId)
                .ToListAsync();

            if (attends == null)
            {
                return NotFound();
            }

            return attends;
        }







        // PUT: api/Attends/5
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
