using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIUser2_0;

namespace APIUser2_0.Controllers
{
    [Produces("application/json")]
    [Route("api/Subscribes")]
    public class SubscribesController : Controller
    {
        private readonly UserDbContext _context;

        public SubscribesController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Subscribes
        [HttpGet]
        public IEnumerable<Subscribe> GetSubscribe()
        {
            return _context.Subscribe;
        }

        // GET: api/Subscribes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscribe([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subscribe = await _context.Subscribe.SingleOrDefaultAsync(m => m.Subscriber == id);

            if (subscribe == null)
            {
                return NotFound();
            }

            return Ok(subscribe);
        }

        // PUT: api/Subscribes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscribe([FromRoute] Guid id, [FromBody] Subscribe subscribe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subscribe.Subscriber)
            {
                return BadRequest();
            }

            _context.Entry(subscribe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscribeExists(id))
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

        // POST: api/Subscribes
        [HttpPost]
        public async Task<IActionResult> PostSubscribe([FromBody] Subscribe subscribe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Subscribe.Add(subscribe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubscribeExists(subscribe.Subscriber))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubscribe", new { id = subscribe.Subscriber }, subscribe);
        }

        // DELETE: api/Subscribes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscribe([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subscribe = await _context.Subscribe.SingleOrDefaultAsync(m => m.Subscriber == id);
            if (subscribe == null)
            {
                return NotFound();
            }

            _context.Subscribe.Remove(subscribe);
            await _context.SaveChangesAsync();

            return Ok(subscribe);
        }

        private bool SubscribeExists(Guid id)
        {
            return _context.Subscribe.Any(e => e.Subscriber == id);
        }
    }
}