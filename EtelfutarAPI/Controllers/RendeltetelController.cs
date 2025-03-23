using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RendeltetelController : Controller
    {
        [HttpGet("GetRendeltetelAsync")]
        public async Task<IActionResult> GetRendeletelAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Rendeles> rendelesek = context.Rendeles.Include(x=>x.Etels).ToList();
                    List<EtelRendelesDTO> etelRendeles = new List<EtelRendelesDTO>();

                    foreach (var rendeles in rendelesek)
                    {
                        foreach (var item in rendeles.Etels)
                        {
                            etelRendeles.Add(new EtelRendelesDTO(item.Id, rendeles.Id));
                        }
                    }
                    return Ok(etelRendeles);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("PostRendeltetelAsync")]
        public async Task<IActionResult> PostRendeltetelAsync(int etelId, int felhasznaloId)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Etelek? etel = await context.Eteleks
                        .FirstOrDefaultAsync(x => x.Id == etelId);
                    Felhasznalok? felhasznalo = await context.Felhasznaloks.Include(x=>x.Rendeles).FirstOrDefaultAsync(x=>x.Id == felhasznaloId);
                    
                    if (etel is not null && felhasznalo is not null)
                    {
                        if (felhasznalo.Rendeles == null)
                        {
                            await context.Rendeles.AddAsync(new Rendeles()
                            {
                                Id = 0,
                                FelhasznaloId = felhasznalo.Id
                            });
                            await context.SaveChangesAsync();
                        }
                        Rendeles rendeles = await context.Rendeles.Include(x=>x.Etels).FirstOrDefaultAsync(x =>x.FelhasznaloId == felhasznalo.Id);
                        if (!rendeles.Etels.Contains(etel))
                        {
                            rendeles.Etels.Add(etel);
                            await context.SaveChangesAsync();
                            return Ok("Sikeres mentés");
                        }
                        else
                        {
                            return BadRequest("Már tartalmazza ezt az ételt a rendelés!");
                        }
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
        [HttpDelete("DeleteRendeltetelAsync")]
        public async Task<IActionResult> DeleteRendeltetelAsync(int etelId, int rendelesId)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Etelek? etel = await context.Eteleks.FirstOrDefaultAsync(x => x.Id == etelId);
                    Rendeles? rendeles = await context.Rendeles.Include(x => x.Etels).FirstOrDefaultAsync(x => x.Id == rendelesId);
                    if (etel is not null && rendeles is not null)
                    {
                        rendeles.Etels.Remove(etel);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return StatusCode(404, "nincs találat");
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
