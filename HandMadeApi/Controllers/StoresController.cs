﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandMadeApi.Models.StoreDatabase;
using Microsoft.AspNetCore.Authorization;

namespace HandMadeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly StoreContext _context;

        public StoresController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Stores
        [HttpGet]
        //[Authorize("read:store")]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        //[Authorize("read:store")]
        public async Task<ActionResult<Store>> GetStore(string id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }
        
        [HttpGet("{id}/products")]
        //[Authorize("read:store")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }
            var products = await _context.Products.Where(p => p.StoreID == id).Include(e=>e.Category).ToListAsync();


            return products;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize("update:store")]
        public async Task<IActionResult> PutStore(string id, Store store)
        {
            if (id != store.ID)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
       // [Authorize("post:store")]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            _context.Stores.Add(store);
            try
            {
                await _context.SaveChangesAsync();
                Client c1 = await _context.Clients.Where(e => e.ID == store.ID).SingleOrDefaultAsync();
                _context.Clients.Remove(c1);
                await _context.SaveChangesAsync();
                ClientsController.updateRole(store.ID, "rol_A4MqRdrRIF1ZiPeE");
            }
            catch (DbUpdateException)
            {
                if (StoreExists(store.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            
            return CreatedAtAction("GetStore", new { id = store.ID }, store);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        //[Authorize("delete:store")]
        public async Task<IActionResult> DeleteStore(string id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(string id)
        {
            return _context.Stores.Any(e => e.ID == id);
        }
    }
}
