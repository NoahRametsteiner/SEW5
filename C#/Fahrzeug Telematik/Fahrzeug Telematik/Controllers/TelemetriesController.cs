using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fahrzeug_Telematik.Models;

namespace Fahrzeug_Telematik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetriesController : ControllerBase
    {
        private readonly Context _context;

        public TelemetriesController(Context context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET: api/Telemetries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telemetry>>> GetTelemetrys()
        {
            return await _context.Telemetrys.ToListAsync();
        }

        // GET: api/Telemetries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Telemetry>> GetTelemetry(long id)
        {
            var telemetry = await _context.Telemetrys.FindAsync(id);

            if (telemetry == null)
            {
                return NotFound();
            }

            return telemetry;
        }

        // PUT: api/Telemetries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelemetry(long id, Telemetry telemetry)
        {
            if (id != telemetry.Id)
            {
                return BadRequest();
            }

            telemetry.ModifiedAt = DateTime.Now;

            telemetry.CreatedAt = _context.Telemetrys.Find(id).CreatedAt;
            _context.Entry(telemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelemetryExists(id))
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

        // POST: api/Telemetries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Telemetry>> PostTelemetry(Telemetry telemetry)
        {
            telemetry.CreatedAt = DateTime.Now;
            _context.Telemetrys.Add(telemetry);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTelemetry", new { id = telemetry.Id }, telemetry);
        }

        // DELETE: api/Telemetries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelemetry(long id)
        {
            var telemetry = await _context.Telemetrys.FindAsync(id);
            if (telemetry == null)
            {
                return NotFound();
            }

            _context.Telemetrys.Remove(telemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelemetryExists(long id)
        {
            return _context.Telemetrys.Any(e => e.Id == id);
        }
    }
}
