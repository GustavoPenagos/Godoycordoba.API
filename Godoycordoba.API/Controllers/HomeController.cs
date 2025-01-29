using Godoycordoba.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Godoycordoba.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        public GodoycordobaContext _context;

        public HomeController(GodoycordobaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/GetUser")]
        public async Task<List<Usuario>> GetUser()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet]
        [Route("/api/GetUserXid")]
        public async Task<Usuario> Index(string documento)
        {
            return await _context.Usuarios.FirstAsync(x => x.Documento.Equals(documento));
        }

        [HttpPost]
        [Route("/api/SaveUser")]
        public async Task<ActionResult> SaveUser(Usuario usuario)
        {
            try
            {
                var save = _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/api/UpdateUser")]
        public async Task<ActionResult> UpdateUser(Usuario usuario)
        {
            try
            {
                var item = _context.Usuarios.First(x=>x.Documento == usuario.Documento);
                item.FechaIngreso = usuario.FechaIngreso;
                item.Nombres = usuario.Nombres;
                item.Apellidos = usuario.Apellidos;
                await _context.SaveChangesAsync();
                await _context.SaveChangesAsync();
                return Ok();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/api/DeleteUser")]
        public async Task<ActionResult> DeleteUser(int tipo, string documento)
        {
            try
            {
                var item = _context.Usuarios.First(x=>x.Documento.Equals(documento) && x.IdTipoDocumento == tipo);
                _context.Usuarios.Remove(item);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
