using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

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
                    List<ChainDTO> response = result.Select(x => new ChainDTO(x)).ToList();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("PostChainAsync")]
        public async Task<IActionResult> PostChainAsync(ChainPostDTO ujChain)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujChain is not null)
                    {
                        await context.Chains.AddAsync(new Chain(ujChain));
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
        public async Task<IActionResult> PutChainAsync(ChainPutDTO modChain)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Chain chain = new Chain(modChain);
                    if (context.Chains.Contains(chain))
                    {
                        context.Chains.Update(chain);
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
