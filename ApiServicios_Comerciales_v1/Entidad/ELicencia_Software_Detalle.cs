using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServicios_Comerciales_v1.Entidad
{
    public class ELicencia_Software_Detalle
    {
        public string id_licencia_detalle { get; set; }
        public string id_modulo { get; set; }
        public string nombre { get; set; }
        public string detalle { get; set; }
        public decimal precio { get; set; }
    }
}
