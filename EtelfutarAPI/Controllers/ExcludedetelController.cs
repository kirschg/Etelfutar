using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExcludedetelController : Controller
    {
        [HttpGet("GetExcludedetelAsync")]
        
        public async Task<IActionResult> GetExcludedetelAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    //List<Ettermek> ettermek = context.Ettermeks.ToListAsync();
                    List<ExcludedEtelDTO> etelek = new List<ExcludedEtelDTO>();
                    return Ok(context);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        

        [HttpPost("PostExcludedetelAsync")]
        public async Task<IActionResult> PostExcludedetelAsync(int etteremId, int etelId)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Etelek? etel = await context.Eteleks.FirstOrDefaultAsync(x => x.Id == etelId);
                    Ettermek? etterem = await context.Ettermeks.FirstOrDefaultAsync(x => x.Id == etteremId);
                    if (etel is not null && etterem is not null)
                    {
                        etterem.Etels.Add(etel);
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
        [HttpDelete("DeleteExcludedetelAsync")]
        public async Task<IActionResult> DeleteExcludedetelAsync(int etteremId, int etelId)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Etelek? etel = await context.Eteleks.FirstOrDefaultAsync(x => x.Id == etelId);
                    Ettermek? etterem = await context.Ettermeks.FirstOrDefaultAsync(x => x.Id == etteremId);
                    if (etel is not null && etterem is not null)
                    {
                        etterem.Etels.Remove(etel);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return StatusCode(404, "Nincs találat");
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
