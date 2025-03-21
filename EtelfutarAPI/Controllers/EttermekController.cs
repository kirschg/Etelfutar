using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EttermekController : Controller
    {
        [HttpGet("GetEttermekAsync")]
        public async Task<IActionResult> GetEttermekAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Ettermek> result = await context.Ettermeks.Include(x=>x.Varos).Include(x=>x.Chain).ToListAsync();
                    List<EttermekDTO> ettermekDTOs = result.Select(x => new EttermekDTO(x)).ToList();
                    return Ok(ettermekDTOs);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("PostEtteremAsync")]
        public async Task<IActionResult> PostEtteremAsync(Ettermek ujEtterem)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujEtterem is not null)
                    {
                        await context.Ettermeks.AddAsync(ujEtterem);
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
        [HttpPut("PutEtteremAsync")]
        public async Task<IActionResult> PutEtteremAsync(Ettermek modEtterem)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (context.Ettermeks.Contains(modEtterem))
                    {
                        context.Ettermeks.Update(modEtterem);
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
        [HttpDelete("DeleteEtteremAsync")]
        public async Task<IActionResult> DeleteEtteremAsync(int id)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Ettermek torlendo = new Ettermek
                    {
                        Id = id
                    };
                    if (context.Ettermeks.Contains(torlendo))
                    {
                        context.Ettermeks.Remove(torlendo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return StatusCode(404, "Nincs ilyen étterem.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpGet("GetEttermekByVaros")]
        public async Task<IActionResult> GetEttermekByVaros(string varos)
        {
            using (var context = new EtelfutarContext())
            {
                List<Ettermek> ettermek = await context.Ettermeks.Where(e => e.Varos.Nev == varos).Include(x => x.Varos).Include(x => x.Chain).Include(x => x.Ertekeleseks).ToListAsync();
                if(ettermek.Count != 0)
                {
                    List<EttermekDTO> ettermekDTOs = ettermek.Select(x => new EttermekDTO(x)).ToList();
                    return Ok(ettermekDTOs);
                }
                else
                {
                    return NotFound("Nincs ilyen város!");
                }
            }
        }
    }
}
