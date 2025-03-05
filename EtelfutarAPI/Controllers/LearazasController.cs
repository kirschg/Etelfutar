using EtelfutarAPI.DTOs;
using EtelfutarAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtelfutarAPI.Controllers
{
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
                    List<EttermekDTO> ettermekDTOs = result.Select(x => new EttermekDTO(x)).ToList();
                    return Ok(ettermekDTOs);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
