using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using STSPro.Data;
using STSPro.Model;

namespace STSPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AdminsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Admins
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Admin>>> Getadmins()
        {
            return await _context.admins.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        
        [HttpPost]
        
        public async Task<ActionResult<Admin>> PostAdmin([FromBody]Admin admin)
        {
            if (admin == null) { return BadRequest("Please enter some data"); }
            if (_context.admins.Any(n => n.Email == admin.Email) == true) { return BadRequest("User already exist"); }
            _context.admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.Id }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.admins.Any(e => e.Id == id);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] Admin admin)
        {
            if (admin == null) { return BadRequest("please enter e-mail and pass"); }
            if (_context.admins.Any(n => n.Email == admin.Email && n.Password == admin.Password))
            {
               
               
               
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var authclainm = new List<Claim>
                    {
                        new Claim(ClaimTypes.UserData,admin.Password),
                        new Claim(ClaimTypes.Email,admin.Email),
                       

                    };
                    var token = new JwtSecurityToken(
                       claims: authclainm,

                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddSeconds(10),

                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                    var data = _context.admins.Where(n => n.Email == admin.Email).ToList().First();

                    return Ok(new
                    {
                        
                        data.Id,              
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                        
            
                    }); ;
                }
                return Unauthorized();

            }

         
              





        
    }
}
    

