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
using STSPro.Model.ViewModel;

namespace STSPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimCardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SimCardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SimCards
        [HttpGet]
        [Authorize]
        public  ActionResult GetsimCards()
        {
           

            //var result = (from simCard in _context.simCards
            //             join userModel in _context.userModels on simCard.UserId equals userModel.Id
            //             join provider in _context.providers on simCard.ProviderId equals provider.Id
            //             join device in _context.devices on simCard.DeviceId equals device.Id
            //             select new
            //             { 
            //                 simCard.Number,
            //                 simCard.IsActiveUser,
            //                 userModel.FirstName,
            //                 userModel.LastName,
            //                 provider.ProviderName,
            //                 device.DeviceName
                            
            //             }).ToList();
            var result = (from simCard in _context.simCards
                         join userModel in _context.userModels on simCard.UserId equals userModel.Id
                         join provider in _context.providers on simCard.ProviderId equals provider.Id
                         join device in _context.devices on simCard.DevicesId equals device.Id
                         select new
                         {
                             Number = simCard.Number,
                             IsActiveUser = simCard.IsActiveUser,
                             FirstName = userModel.FirstName,
                             LastName = userModel.LastName,
                             ProviderName = provider.ProviderName,
                             DeviceName = device.DeviceName
                         }).ToList();


            return Ok(result);
        }

        // GET: api/SimCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SimCard>> GetSimCard(int id)
        {
            var simCard = await _context.simCards.FindAsync(id);

            if (simCard == null)
            {
                return NotFound();
            }

            return simCard;
        }

        // PUT: api/SimCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimCard(int id, SimCard simCard)
        {
            if (id != simCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(simCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimCardExists(id))
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

        // POST: api/SimCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SimCard>> PostSimCard([FromBody]SimViewModel simViewModel)
        {
            if (simViewModel == null) { return BadRequest("enter all data"); }
            if(_context.simCards.Any(num => num.Number == simViewModel.Number) == true) {
                return
                     BadRequest("sim number already exist");
            }
            var simcard = new SimCard();
            simcard.Number = simViewModel.Number;
            simcard.UserId = simViewModel.UserId;
            simcard.IsActiveUser = simViewModel.IsActiveUser==0?"1":"0";

            simcard.ProviderId = simViewModel.ProviderId;
            simcard.User = _context.userModels.Find(simViewModel.UserId);
            simcard.ProviderModel = _context.providers.Find(simViewModel.ProviderId);
            simcard.Devices = _context.devices.Find(simViewModel.DeviceId);

            _context.simCards.Add(simcard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSimCard", new { id = simcard.Id }, simcard);
        }

        // DELETE: api/SimCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSimCard(int id)
        {
            var simCard = await _context.simCards.FindAsync(id);
            if (simCard == null)
            {
                return NotFound();
            }

            _context.simCards.Remove(simCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SimCardExists(int id)
        {
            return _context.simCards.Any(e => e.Id == id);
        }
    }
}
