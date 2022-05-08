using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServicios_Comerciales_v1.Entidad;

namespace ApiServicios_Comerciales_v1.Entidad
{
    public class EUsuario_Licencia_login
    {
        public int estado { get; set; }
        public string mensaje { get; set; }
        public EUsuario usuario { get; set; }
        public ELicencia_Software licencia_software { get; set; }
    }
}
