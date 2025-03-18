using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErtekelesController : Controller
    {
        [HttpGet("GET/Értékelés")]
        public async Task<IActionResult> Get()
        {
            using (var context = new EtelfutarContext())
            {
                List<Ertekelesek> adat = await context.Ertekeleseks.ToListAsync();
                return Ok(adat);
            }
        }
        [HttpPost("POST/Értékelés")]
        public async Task<IActionResult> Post(Ertekelesek ujErtekeles)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujErtekeles is not null)
                    {
                        await context.Ertekeleseks.AddAsync(ujErtekeles);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres Mentés!");
                    }
                    else
                    {
                        return NotFound("Üres objektumot kaptam!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Hiba!" + ex.Message);
                }
            }
        }
        [HttpPut("PUT/Értékelés")]
        public async Task<IActionResult> Put(Ertekelesek modositottErtekeles)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (context.Ertekeleseks.Contains(modositottErtekeles))
                    {
                        context.Ertekeleseks.Update(modositottErtekeles);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres módosítás!");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen értékelés");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpDelete("DELETE/Értékelés")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Ertekelesek torlendo = new Ertekelesek()
                    {
                        Id = id
                    };
                    if (context.Ertekeleseks.Contains(torlendo))
                    {
                        context.Ertekeleseks.Remove(torlendo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés!");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen Értékelés!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
