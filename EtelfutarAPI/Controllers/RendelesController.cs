using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RendelesController : Controller
    {

        [CustomAuthorize]
        [HttpGet("GetRendelesekAsync")]
        public async Task<IActionResult> GetRendelesekAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Rendeles> result = await context.Rendeles.Include(x => x.Felhasznalo).Include(x=>x.Felhasznalo.Varos).ToListAsync();
                    List<RendelesDTO> rendelesDTOs = result.Select(x => new RendelesDTO(x)).ToList();
                    return Ok(rendelesDTOs);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("PostRendelesAsync")]
        public async Task<IActionResult> PostRendelesAsync(Rendeles ujRendeles)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujRendeles is not null)
                    {
                        await context.Rendeles.AddAsync(ujRendeles);
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
        [HttpPut("PutVarosAsync")]
        public async Task<IActionResult> PutRendelesAsync(Rendeles modRendeles)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (context.Rendeles.Contains(modRendeles))
                    {
                        context.Rendeles.Update(modRendeles);
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
        [HttpDelete("DeleteRendelesAsync")]
        public async Task<IActionResult> DeleteRendelesAsync(int id)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Rendeles torlendo = new Rendeles
                    {
                        Id = id
                    };
                    if (context.Rendeles.Contains(torlendo))
                    {
                        context.Rendeles.Remove(torlendo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen város.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpGet("GetByFelhasznaloNev")]
        public async Task<IActionResult> GetRendelesByFelhasznaloNev(string FelhasznaloNev)
        {
            using (var context = new EtelfutarContext())
            {
                Felhasznalok? felhasznalo = await context.Felhasznaloks.FindAsync(FelhasznaloNev);

                if (FelhasznaloNev != null)
                {
                    List<Felhasznalok> rendelesek = await context.Rendeles.FirstOrDefaultAsync(x => !x.Felhasznalo.Contains(felhasznalo) && x.FelhasznaloId == felhasznalo.Id).Include(x => x.Chain).ToListAsync();
                    List<RendelesFelhasznalokDTO> rendelesDTOs = rendelesek.Select(x => new RendelesFelhasznalokDTO(x)).ToList();
                    return Ok(rendelesDTOs);
                }
                else
                {
                    return NotFound("Nincs ilyen étterem!");
                }
            }
        }
    }
}
