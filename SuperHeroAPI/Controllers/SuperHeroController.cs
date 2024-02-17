using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {


        private static List<SuperHero> Heroes = new List<SuperHero>
        {
            new SuperHero
            {
                Id = 1, Name = "Spiderman",
                FirstName = "Peter" ,
                LastName = "Parker" ,
                Place = "New York City"

            },

            new SuperHero
            {
                Id = 2,
                Name = "Ironman",
                FirstName = "Tony" ,
                LastName = "Stark" ,
                Place = "Long Island"

            },

        };

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {

            this._context = context;

        }

        [HttpGet]

        public async Task<ActionResult<List<SuperHero>>> Get()

        {

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<SuperHero>> GetHero(int id)

        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)

            {

                return BadRequest("Hero not found.");

            }

            return Ok(hero);

        }

        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)

        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)

        {
            var dbhero  = await _context.SuperHeroes.FindAsync(request.Id);

            if (dbhero == null)

            { 

                return BadRequest("Hero not found.");

            }

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await _context.SaveChangesAsync();

            Heroes.Add(dbhero);
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)

        {
            var dbhero = await _context.SuperHeroes.FindAsync(id);

            if (dbhero == null)

            {

                return BadRequest("Hero not found.");

            }

            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

        }


    }

}




