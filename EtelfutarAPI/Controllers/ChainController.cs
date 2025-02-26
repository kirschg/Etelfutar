using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChainController : Controller
    {
        [HttpGet("GetChainAsync")]
        public async Task<IActionResult> GetChainAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Chain> result = await context.Chains.ToListAsync();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("PostChainAsync")]
        public async Task<IActionResult> PostChainAsync(Chain ujChain)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujChain is not null)
                    {
                        await context.Chains.AddAsync(ujChain);
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
        [HttpPut("PutChainAsync")]
        public async Task<IActionResult> PutChainAsync(Chain modChain)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (context.Chains.Contains(modChain))
                    {
                        context.Chains.Update(modChain);
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
        [HttpDelete("DeleteChainAsync")]
        public async Task<IActionResult> DeleteChainAsync(int id)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Chain torlendo = new Chain
                    {
                        Id = id
                    };
                    if (context.Chains.Contains(torlendo))
                    {
                        context.Chains.Remove(torlendo);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres törlés");
                    }
                    else
                    {
                        return StatusCode(404, "Nincs ilyen chain.");
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
