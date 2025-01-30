using Godoycordoba.Data.Context;
using Godoycordoba.Data.Model.Models;
using Godoycordoba.Data.Model.Request;
using Godoycordoba.Data.Model.Response;
using Godoycordoba.Logic.Interfaces;
using Godoycordoba.Logic.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Services;
using System.Collections.Immutable;

namespace Godoycordoba.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        public readonly GodoycordobaContext _context;
        private readonly ILogic _Ilogic;
        private readonly IConfiguration _configuration;
        string messageDescription = "Exitoso";

        public HomeController(GodoycordobaContext context, ILogic logic, IConfiguration configuration)
        {
            this._context = context;
            this._Ilogic = logic;
            this._configuration = configuration;
        }

        #region Methods
        [HttpGet]
        [Route("/api/GetUser")]
        public async Task<ResponseUsuario> GetUser()
        {
            try
            {
                var item = await _context.Usuarios.ToArrayAsync();
                return MapResponse.ResponseMessage(200, item, messageDescription, true);
            }
            catch (Exception ex)
            {
                messageDescription = ex.Message;
                return MapResponse.ResponseMessage(500, null, messageDescription, false);
            }

        }

        [HttpGet]
        [Route("/api/GetUserXid")]
        public async Task<ResponseUsuario> GetUserXid(string documento)
        {
            try
            {
                var item = await _context.Usuarios.FirstAsync(x => x.Documento.Equals(documento));
                return MapResponse.ResponseMessage(200, item, messageDescription, true);

            }
            catch (Exception ex)
            {
                messageDescription = ex.Message;
                return MapResponse.ResponseMessage(500, null, messageDescription, false);
            }
        }

        [HttpPost]
        [Route("/api/SaveUser")]
        public async Task<ResponseUsuario> SaveUser(Usuario usuario)
        {
            try
            {
                string ValidEmail = ValidarCorreo(usuario.CorreoElentronico);
                if (!string.IsNullOrEmpty(ValidEmail))
                {
                    return MapResponse.ResponseMessage(500, null, ValidEmail, false);
                }
                int score = _Ilogic.SetScore(usuario.Nombres, usuario.Apellidos);
                int emailScore = _Ilogic.EmailScore(usuario.CorreoElentronico);
                usuario.Puntaje = score + emailScore;
                usuario.FechaIngreso = DateTime.Now;
                var save = _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                var item = await _context.Usuarios.FirstAsync(x => x.Documento.Equals(usuario.Documento));
                return MapResponse.ResponseMessage(200, item, messageDescription, true);
            }
            catch (Exception ex)
            {
                messageDescription = ex.Message;
                return MapResponse.ResponseMessage(500, null, messageDescription, false);
            }
        }

        [HttpPut]
        [Route("/api/UpdateUser")]
        public async Task<ResponseUsuario> UpdateUser([FromBody] UsuarioDto usuario, string documento)
        {
            try
            {
                var item = await _context.Usuarios.FirstAsync(x => x.Documento == documento);

                if (!string.IsNullOrEmpty(usuario.Nombres))
                    item.Nombres = usuario.Nombres;
                if (!string.IsNullOrEmpty(usuario.Apellidos))
                    item.Apellidos = usuario.Apellidos;
                if (!string.IsNullOrEmpty(usuario.CorreoElentronico))
                    item.CorreoElentronico = usuario.CorreoElentronico;

                await _context.SaveChangesAsync();

                return MapResponse.ResponseMessage(200, item, messageDescription, true);

            }
            catch (Exception ex)
            {
                messageDescription = ex.Message;
                return MapResponse.ResponseMessage(500, null, messageDescription, false);
            }
        }

        [HttpDelete]
        [Route("/api/DeleteUser")]
        public async Task<ResponseUsuario> DeleteUser(int tipo, string documento)
        {
            try
            {
                var item = _context.Usuarios.First(x => x.Documento.Equals(documento) && x.IdTipoDocumento == tipo);
                _context.Usuarios.Remove(item);
                await _context.SaveChangesAsync();
                return MapResponse.ResponseMessage(200, null, messageDescription, true);

            }
            catch (Exception ex)
            {
                messageDescription = ex.Message;
                return MapResponse.ResponseMessage(500, null, messageDescription, false);
            }
        }



        [HttpPost]
        [Route("/api/Login")]
        public async Task<ResponseUsuario> Login(Login login)
        {
            List<MUsuario> usuarios = [];
            try
            {
                var item = await _context.Usuarios.FirstAsync(x => x.CorreoElentronico.Equals(login.Username) && x.Documento.Equals(login.Password));
                if (item == null)
                {
                    messageDescription = string.Format("Usuario o contraseña incorrecta: {0}", login.Username);
                    return MapResponse.ResponseMessage(500, null, messageDescription, false);
                }
                string clasificar = _Ilogic.Classify(item.FechaIngreso);
                if (!await _Ilogic.UpdateLoginDate(item))
                {
                    messageDescription = string.Format("No se pudo actualizar la fecha del usuario: {0}", login.Username);
                }
                MapearDatosRespuesta(ref usuarios, item);
                messageDescription = string.Format("login exitoso con el usuario: {0} de clasificacion {1}", login.Username, clasificar);

                return MapResponse.ResponseMessage(200, usuarios, messageDescription, true);

            }
            catch (Exception ex)
            {
                messageDescription = ex.Message;
                return MapResponse.ResponseMessage(500, null, messageDescription, false);
            }

        }

        #endregion


        #region Validations
        private void MapearDatosRespuesta(ref List<MUsuario> usuarios, Usuario item)
        {
            usuarios.Add(new MUsuario
            {
                Id = item.Id,
                Apellidos = item.Apellidos,
                CorreoElentronico = item.CorreoElentronico,
                Documento = item.Documento,
                FechaIngreso = Convert.ToDateTime(item.FechaIngreso),
                IdTipoDocumento = item.IdTipoDocumento,
                Nombres = item.Nombres,
                Puntaje = item.Puntaje
            });
        }

        private string ValidarCorreo(string correoElentronico)
        {
            string messge = "";
            if (!correoElentronico.Contains("@"))
                messge = "El correo NO contiene @, ";
            var extensions = _configuration["Extenciones:DU"];
            var split = extensions?.Split(":");
            if (!correoElentronico.Contains(split[0]) && !correoElentronico.Contains(split[1]))
                messge += string.Format("No contiene {0} o {1}", split[0], split[1]);
            
            return messge;
        } 
        #endregion
    }
}
