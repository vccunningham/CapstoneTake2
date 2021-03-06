﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneTake2.Data;
using CapstoneTake2.Models;

namespace CapstoneTake2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase {

        public const string StatusNew      = "NEW";
        public const string StatusEdit     = "EDIT";
        public const string StatusReview   = "REVIEW";
        public const string StatusApproved = "APPROVED";
        public const string StatusRejected = "REJECTED";


        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        [HttpPut("review/{id}")]
        public async Task<IActionResult> review(int id) {

            var request = await _context.Requests.FindAsync(id);

            if (request.Total <= 50) {
                request.Status = "APPROVED";
            } else {
                request.Status = "REVIEW";
            }

            return await PutRequest(id, request);
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;// this was changed.

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        [HttpPut("reviewstatus")]
        public async Task<ActionResult<Request>> Review(Request request) {
            if (request.Total > 50) {

                request.Status = StatusReview;
            } 
            else {
                request.Status = StatusApproved;
            }

            _context.Requests.Add(request);
               await _context.SaveChangesAsync();

            return request;

        }

        [HttpPut("approved")]
        public async Task<ActionResult<Request>> Approved(Request request) {
                
            request = _context.Requests.Find(request.Id);
            request.Status = "Approved";
            
            await _context.SaveChangesAsync();

            return Ok(request);

        }
        [HttpPut("reject")]
        public async Task<ActionResult<Request>> Reject(Request request) {

            request = _context.Requests.Find(request.Id);
            request.Status = "Rejected";

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);

        }

        // POST: api/Requests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
