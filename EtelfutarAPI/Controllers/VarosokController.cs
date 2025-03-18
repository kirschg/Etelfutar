using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VarosokController : Controller
    {
        [CustomAuthorize]
        [HttpGet("GetVarosokAsync")]
        public async Task<IActionResult> GetVarosokAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Varosok> result = await context.Varosoks.ToListAsync();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [CustomAuthorize]
        [HttpPost("PostVarosAsync")]
        public async Task<IActionResult> PostVarosAsync(Varosok ujVaros)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujVaros is not null)
                    {
                        await context.Varosoks.AddAsync(ujVaros);
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
        [CustomAuthorize]
        [HttpPut("PutVarosAsync")]
        public async Task<IActionResult> PutVarosAsync(Varosok modVaros)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (context.Varosoks.Contains(modVaros))
                    {
                        context.Varosoks.Update(modVaros);
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
        [HttpDelete("DeleteVarosAsync")]
        public async Task<IActionResult> DeleteVarosAsync(int id)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Varosok torlendo = new Varosok
                    {
                        Id = id
                    };
                    if (context.Varosoks.Contains(torlendo))
                    {
                        context.Varosoks.Remove(torlendo);
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
    }
}
