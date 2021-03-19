using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Lab1_czarczynski.Models;
using Lab1_czarczynski.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lab1_czarczynski.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PeopleController> _log;

        public PeopleController(ApplicationDbContext context, ILogger<PeopleController> log)
        {
            _context = context;
            _log = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var toReturn = _context.People.ToList();
                return Ok(toReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            try
            {

                var toReturn = _context.People.SingleOrDefault(x => x.PersonId == id);
                if (toReturn == null) throw new Exception($"Cannot find person with id: {id}");
                return Ok(toReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonDTO body)
        {
            try
            {
                if (String.IsNullOrEmpty(body.Firstname) || String.IsNullOrEmpty(body.Lastname)) throw new Exception("Neither firstname or lastname can be empty");
                var toSave = new Person
                {
                    Firstname = body.Firstname,
                    Lastname = body.Lastname
                };
                _context.People.Add(toSave);
                _context.SaveChanges();
                return Ok(toSave);
            }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PersonDTO body)
        {
            try
            {
                var toUpdate = _context.People.SingleOrDefault(x => x.PersonId == id); ;
                if (toUpdate == null) throw new Exception($"Cannot find person with id: {id}");
                toUpdate.Firstname = body.Firstname ?? toUpdate.Firstname;
                toUpdate.Lastname = body.Lastname ?? toUpdate.Lastname;
                _context.SaveChanges();

                return Ok(toUpdate);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                var toDelete = _context.People.SingleOrDefault(x => x.PersonId == id);
                if (toDelete == null) throw new Exception($"Cannot find person with id: {id}");

                _context.People.Remove(toDelete);

                _context.SaveChanges();

                return Ok(new { deleted = toDelete });
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

    }
}
