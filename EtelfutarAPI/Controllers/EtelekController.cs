using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EtelekController : Controller
    {
        [HttpGet("GetEtelekAsync")]
        public async Task<IActionResult> GetEtelekAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Etelek> result = await context.Eteleks.Include(x=>x.Chain).ToListAsync();
                    List<EtelekDTO> etelekDTOs = result.Select(x => new EtelekDTO(x)).ToList();
                    return Ok(etelekDTOs);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("PostEtelAsync")]
        public async Task<IActionResult> PostEtelAsync(Etelek ujEtel)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujEtel is not null)
                    {
                        await context.Eteleks.AddAsync(ujEtel);
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
        [HttpPut("PutEtelAsync")]
        public async Task<IActionResult> PutEtelAsync(Etelek modEtel)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (context.Eteleks.Contains(modEtel))
                    {
                        context.Eteleks.Update(modEtel);
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
        [HttpDelete("DeleteEtelAsync")]
        public async Task<IActionResult> DeleteEtelAsync(int id)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Etelek torlendo = new Etelek
                    {
                        Id = id
                    };
                    if (context.Eteleks.Contains(torlendo))
                    {
                        context.Eteleks.Remove(torlendo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return StatusCode(404, "Nincs ilyen étel.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpGet("GetEtelekByEtterem")]
        public async Task<IActionResult> GetEtelekByEtterem(int etteremId)
        {
            using (var context = new EtelfutarContext())
            {
                Ettermek? etterem = await context.Ettermeks.FindAsync(etteremId);

                if (etterem != null)
                {
                    List<Etelek> etelek = await context.Eteleks.Where(x => !x.Etterems.Contains(etterem) && x.ChainId == etterem.ChainId).Include(x=>x.Chain).ToListAsync();
                    List<EtelekDTO> etelekDTOs = etelek.Select(x => new EtelekDTO(x)).ToList();
                    return Ok(etelekDTOs);
                }
                else
                {
                    return NotFound("Nincs ilyen étterem!");
                }
            }
        }
    }
}
