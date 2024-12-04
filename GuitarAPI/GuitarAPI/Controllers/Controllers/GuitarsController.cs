using Microsoft.AspNetCore.Mvc;
using GuitarAPI.Controllers.Models; // Подключаем модель Guitar
using System.Collections.Generic;

namespace GuitarAPI.Controllers.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuitarsController : ControllerBase
    {
        // Хранилище гитар (вместо базы данных)
        private static List<Guitar> Guitars = new List<Guitar>
        {
            new Guitar { Id = 1, Model = "Telecaster", Manufacturer = "Fender" },
            new Guitar { Id = 2, Model = "Stratocaster", Manufacturer = "Fender" },
            new Guitar { Id = 3, Model = "Les Paul", Manufacturer = "Gibson" },
            new Guitar { Id = 4, Model = "SG", Manufacturer = "Gibson" }
        };

        // GET: api/guitars
        [HttpGet]
        public ActionResult<IEnumerable<Guitar>> GetGuitars()
        {
            return Guitars;
        }

        // GET: api/guitars/{id}
        [HttpGet("{id}")]
        public ActionResult<Guitar> GetGuitar(int id)
        {
            var guitar = Guitars.Find(g => g.Id == id);
            if (guitar == null) return NotFound();
            return guitar;
        }

        // POST: api/guitars
        [HttpPost]
        public ActionResult<Guitar> PostGuitar(Guitar guitar)
        {
            guitar.Id = Guitars.Count > 0 ? Guitars[^1].Id + 1 : 1;
            Guitars.Add(guitar);
            return CreatedAtAction(nameof(GetGuitar), new { id = guitar.Id }, guitar);
        }

        // PUT: api/guitars/{id}
        [HttpPut("{id}")]
        public IActionResult PutGuitar(int id, Guitar guitar)
        {
            var existingGuitar = Guitars.Find(g => g.Id == id);
            if (existingGuitar == null) return NotFound();

            existingGuitar.Model = guitar.Model;
            existingGuitar.Manufacturer = guitar.Manufacturer;

            return NoContent();
        }

        // GET: api/Guitars/manufacturer/{manufacturer}
        [HttpGet("manufacturer/{manufacturer}")]
        public ActionResult<IEnumerable<Guitar>> GetByManufacturer(string manufacturer)
        {
            // Фильтруем список гитар по производителю
            var filteredGuitars = Guitars.Where(g => g.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!filteredGuitars.Any())
            {
                return NotFound($"No guitars found for manufacturer: {manufacturer}");
            }

            return Ok(filteredGuitars);
        }

        // DELETE: api/guitars/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteGuitar(int id)
        {
            var guitar = Guitars.Find(g => g.Id == id);
            if (guitar == null) return NotFound();

            Guitars.Remove(guitar);
            return NoContent();
        }


    }
}