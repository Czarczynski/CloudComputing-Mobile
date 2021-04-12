using System;
using System.Linq;
using Lab4_app.API;
using Lab4_app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lab4_app.API.Controllers
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
        public IActionResult GetPeople()
        {
            return Ok(_context.People);
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody] Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}/photo")]
        public IActionResult GetPhoto([FromRoute] int id)
        {
            var p = _context.People.First(w => w.PersonId == id);
            return base.File(Convert.FromBase64String(p.PictureBase64), "image/jpeg");
        }

    }
}
