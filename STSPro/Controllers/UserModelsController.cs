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
    public class UserModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserModels
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetuserModels()
        {
            return await _context.userModels.ToListAsync();
        }

        // GET: api/UserModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(int id)
        {
            var userModel = await _context.userModels.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return userModel;
        }

        // PUT: api/UserModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel(int id, UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(userModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        // POST: api/UserModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUserModel([FromBody]UserModel userModel)
        {
            if (userModel == null) { return BadRequest("Please enter some data"); }
            if(_context.userModels.Any(n => n.Email == userModel.Email) == true) { return  BadRequest("User already exist"); }
            _context.userModels.Add(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
        }

        // DELETE: api/UserModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel(int id)
        {
            var userModel = await _context.userModels.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.userModels.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserModelExists(int id)
        {
            return _context.userModels.Any(e => e.Id == id);
        }
    }
}
