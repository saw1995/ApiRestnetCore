using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServicios_Comerciales_v1.Negocio;
using ApiServicios_Comerciales_v1.Entidad;
using System.ComponentModel.DataAnnotations;

namespace ApiServicios_Comerciales_v1.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("registro_usuario_cliente")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ERespuesta> registro_usuario_cliente([FromForm] string email, [FromForm] string nombre,
            [FromForm] string apellido_paterno, [FromForm] string apellido_materno, [FromForm] string contrasenna)
        {
            var res = await new NUsuario().registrar_usuario_cliente(email, nombre, apellido_paterno, apellido_materno, contrasenna);
            return res;
        }

        [HttpPost("login_usuario_by_ci_email")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<EUsuario_Licencia_login> login_usuario_by_ci_email([FromForm] string texto, [FromForm] string contrasenna)
        {
            return await new NUsuario().login_usuario_by_ci_email(texto, contrasenna);
        }

        //hasta aqui el codigo net de C#
    }
}