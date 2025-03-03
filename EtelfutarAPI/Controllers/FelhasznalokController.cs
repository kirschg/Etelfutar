using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FelhasznalokController : Controller
    {
        [HttpGet("GetFelhasznalokAsync")]
        public async Task<IActionResult> GetFelhasznalokAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Felhasznalok> result = await context.Felhasznaloks.Include(x=>x.Varos).ToListAsync();
                    List<FelhasznalokDTO> felhasznalokDTOs = result.Select(x => new FelhasznalokDTO(x)).ToList();
                    return Ok(felhasznalokDTOs);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        /*[HttpPost("PostFelhasznaloAsync")]
        public async Task<IActionResult> PostFelhasznaloAsync(Felhasznalok ujFelhasznalo)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujFelhasznalo is not null)
                    {
                        await context.Felhasznaloks.AddAsync(ujFelhasznalo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres mentés");
                    }
                    else
                    {
                        return BadRequest("Üres objektumot kaptam!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPut("PutFelhasznaloAsync")]
        public async Task<IActionResult> PutFelhasznaloAsync(Felhasznalok modFelhasznalo)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (context.Felhasznaloks.Contains(modFelhasznalo))
                    {
                        context.Felhasznaloks.Update(modFelhasznalo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres módosítás.");
                    }
                    else
                    {
                        return NotFound("Üres objektumot kaptam!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpDelete("DeleteFelhasznaloAsync")]
        public async Task<IActionResult> DeleteFelhasznaloAsync(int id)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Felhasznalok torlendo = new Felhasznalok
                    {
                        Id = id
                    };
                    if (context.Felhasznaloks.Contains(torlendo))
                    {
                        context.Felhasznaloks.Remove(torlendo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen felhasználó.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }*/
    }
}
