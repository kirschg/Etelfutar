using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VizsgaremekAPI;

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
                    List<Felhasznalok> result = await context.Felhasznaloks.Include(x => x.Varos).ToListAsync();
                    List<FelhasznalokDTO> felhasznalokDTOs = result.Select(x => new FelhasznalokDTO(x)).ToList();
                    return Ok(felhasznalokDTOs);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [CustomAuthorize]
        [HttpGet("GetFelhasznaloByTokenAsync")]
        public async Task<IActionResult> GetFelhasznaloByTokenAsync(string token)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Felhasznalok? user = Program.LoggedInUsers[token];
                    if (user != null)
                    {
                        return Ok(new FelhasznaloByTokenDTO(user));
                    }
                    else
                    {
                        return NotFound("Valószinűleg nincs ilyen felhasználó.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Elbasztad: " + ex.Message);
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
        [CustomAuthorize]
        [HttpPut("FelhasznalokPutRestricted")]
        public async Task<IActionResult> FelhasznaloPutRestricted(FelhasznalokPutRestrictedDTO modFelhasznalo)
        {
            using (var context = new EtelfutarContext())
            {

                try
                {
                    Felhasznalok? felhasznalok = await context.Felhasznaloks.FirstOrDefaultAsync(x => x.Id == modFelhasznalo.Id);
                    if (felhasznalok != null)
                    {
                        felhasznalok.FelhasznaloNev = modFelhasznalo.FelhasznaloNev;
                        felhasznalok.TeljesNev = modFelhasznalo.TeljesNev;
                        felhasznalok.Lakcim = modFelhasznalo.Lakcim;
                        felhasznalok.VarosId = modFelhasznalo.VarosId;
                        context.Update(felhasznalok);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres módosítás");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen felhasználó.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("elbasztad:" + ex.Message);
                }
            }
    }
}
}
