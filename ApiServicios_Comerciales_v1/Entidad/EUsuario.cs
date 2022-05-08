using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServicios_Comerciales_v1.Entidad
{
    public class EUsuario : EUsuarioRol
    {
        public string id_usuario { get; set; }
        public bool usuario_administrador { get; set; }
        public string ci { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string foto { get; set; }
        public bool estado { get; set; }
    }
}
