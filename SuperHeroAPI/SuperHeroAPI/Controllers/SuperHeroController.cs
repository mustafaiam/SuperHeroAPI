﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero {
                    Id = 2,
                    Name = "Ironman",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "Long Island"
                }
            };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);

            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = heroes.Find(h => h.Id == request.Id);

            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            return Ok(heroes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = heroes.Find(h => h.Id == id);

            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }

            heroes.Remove(hero);

            return Ok(heroes);
        }
    }
}
