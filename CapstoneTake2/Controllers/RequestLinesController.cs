using System;
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
    public class RequestLinesController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public RequestLinesController(PrsDbContext context)
        {
            _context = context;
        }

        public void RecalculateTotal(int requestId) {

            var request = _context.Requests.SingleOrDefault(dbrecord => dbrecord.Id == requestId);
                var total = _context.RequestLines
                .Include(l => l.Product)
                .Where(l => l.RequestId == requestId)
                .Sum(l => l.Quantity * l.Product.Price);
            request.Total = total;
               
                 _context.SaveChanges();


            //for loop to calculate the total from each requestline
            //take the total and update the total on the request
            //save the request in the database


        }

        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLines()
        {
            return await _context.RequestLines.ToListAsync();
        }

        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLines(int id)
        {
            var requestLines = await _context.RequestLines.FindAsync(id);

            if (requestLines == null)
            {
                return NotFound();
            }

            return requestLines;
        }

        // PUT: api/RequestLines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLines(int id, RequestLine requestLines)
        {
            if (id != requestLines.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLines).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLinesExists(id))
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
        [HttpPost("insert/{id}")]
        public RequestLine Insert(RequestLine requestLine) {
            if (requestLine == null) throw new Exception("Can't be null");
            _context.RequestLines.Add(requestLine);
            try {
                _context.SaveChanges();
                RecalculateTotal(requestLine.RequestId);
            } catch (DbUpdateException ex) {
                throw new Exception("Must be unique", ex);
            } catch (Exception) {
                throw;
            }
            return requestLine;
        }

        // POST: api/RequestLines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLines(RequestLine requestLines)
        {
            _context.RequestLines.Add(requestLines);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestLines", new { id = requestLines.Id }, requestLines);
        }
        
        [HttpPut("update/{id}")]
        public RequestLine Update(RequestLine requestLine) {
            if (requestLine == null) throw new Exception("Can't be null");
            var DBRequestLine = _context.RequestLines.SingleOrDefault(x => x.Id == requestLine.Id);
            if (DBRequestLine == null) {
                throw new Exception("Can't find Requestline in database!");

            } else {
                try {
                    DBRequestLine.Quantity = requestLine.Quantity;
                    DBRequestLine.ProductId = requestLine.ProductId;
                    int OriginalRequestID = DBRequestLine.RequestId;
                    DBRequestLine.RequestId = requestLine.RequestId;
                    _context.SaveChanges();
                    RecalculateTotal(OriginalRequestID);
                    RecalculateTotal(requestLine.RequestId);
                } catch (DbUpdateException ex) {
                    throw new Exception("Must be unique", ex);
                } catch (Exception) {
                    throw;
                }
            }
            return requestLine;
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestLine>> DeleteRequestLines(int id)
        {
            var requestLines = await _context.RequestLines.FindAsync(id);
            if (requestLines == null)
            {
                return NotFound();
            }

            _context.RequestLines.Remove(requestLines);
            await _context.SaveChangesAsync();

            return requestLines;
        }

        private bool RequestLinesExists(int id)
        {
            return _context.RequestLines.Any(e => e.Id == id);
        }
        [HttpDelete("delete/{id}")]
        public RequestLine Delete(RequestLine requestLine) {
            if (requestLine == null) throw new Exception("Can't be null");
            var DBRequestLine = _context.RequestLines.SingleOrDefault(x => x.Id == requestLine.Id);
            _context.RequestLines.Remove(requestLine);
            try {
                DBRequestLine.Quantity = requestLine.Quantity;
                DBRequestLine.ProductId = requestLine.ProductId;
                int OriginalRequestID = DBRequestLine.RequestId;
                DBRequestLine.RequestId = requestLine.RequestId;
                _context.SaveChanges();
                RecalculateTotal(requestLine.RequestId);
            } catch (DbUpdateException ex) {
                throw new Exception("Must be unique", ex);
            } catch (Exception) {
                throw;
            }
            return requestLine;
        }
    }
}
