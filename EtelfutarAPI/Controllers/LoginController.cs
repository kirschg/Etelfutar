using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VizsgaremekAPI.DTOs;
using EtelfutarAPI.Models;

namespace VizsgaremekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("GetSalt/{felhasznaloNev}")]
        public async Task<IActionResult> GetSalt(string felhasznaloNev)
        {
            using(var context = new EtelfutarContext())
            {
                try
                {
                    Felhasznalok? response = await context.Felhasznaloks.FirstOrDefaultAsync(u=>u.FelhasznaloNev == felhasznaloNev);
                    if(response == null)
                    {
                        return NotFound("Nem található felhasználó ezzel a felhasználó névvel!");
                    }
                    else
                    {
                        return Ok(response.Salt);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            using(var context = new EtelfutarContext())
            {
                try
                {
                    string Hash = Program.CreateSHA256(loginDTO.TmpHash);
                    Felhasznalok? loggedUser = await context.Felhasznaloks
                        .Include(x=>x.Rendeles.Etels)
                        .Include(x=>x.Varos)
                        .FirstOrDefaultAsync(u => u.FelhasznaloNev == loginDTO.LoginName && u.Hash == Hash);
                    if(loggedUser != null && loggedUser.Aktiv == 1)
                    {
                        string token = Guid.NewGuid().ToString();
                        lock (Program.LoggedInUsers)
                        {
                            Program.LoggedInUsers.Add(token, loggedUser);
                        }
                        return Ok(new LoggedUser
                        {
                            FelhasznaloNev = loginDTO.LoginName,
                            Email = loggedUser.Email,
                            VarosId = loggedUser.VarosId,
                            Jogosultsag = loggedUser.Jogosultsag,
                            Token = token
                        });
                    }
                    else
                    {
                        return Unauthorized("Inaktív felhasználó.");
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