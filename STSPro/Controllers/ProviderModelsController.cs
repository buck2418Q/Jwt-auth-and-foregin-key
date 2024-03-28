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
    public class ProviderModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProviderModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProviderModels
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProviderModel>>> Getproviders()
        {
            return await _context.providers.ToListAsync();
        }

        // GET: api/ProviderModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProviderModel>> GetProviderModel(int id)
        {
            var providerModel = await _context.providers.FindAsync(id);

            if (providerModel == null)
            {
                return NotFound();
            }

            return providerModel;
        }

        // PUT: api/ProviderModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProviderModel(int id, ProviderModel providerModel)
        {
            if (id != providerModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(providerModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderModelExists(id))
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

        // POST: api/ProviderModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProviderModel>> PostProviderModel(ProviderModel providerModel)
        {
            _context.providers.Add(providerModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProviderModel", new { id = providerModel.Id }, providerModel);
        }

        // DELETE: api/ProviderModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProviderModel(int id)
        {
            var providerModel = await _context.providers.FindAsync(id);
            if (providerModel == null)
            {
                return NotFound();
            }

            _context.providers.Remove(providerModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderModelExists(int id)
        {
            return _context.providers.Any(e => e.Id == id);
        }
    }
}
