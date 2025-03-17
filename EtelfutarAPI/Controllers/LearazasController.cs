using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LearazasController : Controller
    {
        [HttpGet("GetLearazasAsync")]
        public async Task<IActionResult> GetFelhasznalokAsync()
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    List<Learaza> result = await context.Learazas.Include(x => x.Etel).Include(x => x.Etterem).ToListAsync();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost("PostLearazasAsync")]
        public async Task<IActionResult> PostLearazasAsync(Learaza ujLearazas)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (ujLearazas is not null)
                    {
                        await context.Learazas.AddAsync(ujLearazas);
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
        [HttpPut("PutLearazasAsync")]
        public async Task<IActionResult> PutLearazasAsync(Learaza modLearazas)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    if (modLearazas is not null)
                    {
                        context.Learazas.Update(modLearazas);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres módosítás");
                    }
                    else
                    {
                        return BadRequest("Nincs ilyen leárazás");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpDelete("DeleteLearazasAsync")]
        public async Task<IActionResult> DeleteLearazasAsync(int etteremId, int etelId)
        {
            using (var context = new EtelfutarContext())
            {
                try
                {
                    Learaza? learazas = await context.Learazas.FirstOrDefaultAsync(x => x.EtteremId == etteremId && x.EtelId == etelId);
                    if (learazas is not null)
                    {
                        context.Learazas.Remove(learazas);
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
