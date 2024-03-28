using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STSPro.Data;
using STSPro.Model;

namespace STSPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Devices>>> Getdevices()
        {
            return await _context.devices.ToListAsync();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Devices>> GetDevices(int id)
        {
            var devices = await _context.devices.FindAsync(id);

            if (devices == null)
            {
                return NotFound();
            }

            return devices;
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevices(int id, Devices devices)
        {
            if (id != devices.Id)
            {
                return BadRequest();
            }

            _context.Entry(devices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DevicesExists(id))
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

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Devices>> PostDevices(Devices devices)
        {
            _context.devices.Add(devices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevices", new { id = devices.Id }, devices);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevices(int id)
        {
            var devices = await _context.devices.FindAsync(id);
            if (devices == null)
            {
                return NotFound();
            }

            _context.devices.Remove(devices);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DevicesExists(int id)
        {
            return _context.devices.Any(e => e.Id == id);
        }
    }
}
