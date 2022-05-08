using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServicios_Comerciales_v1.Entidad
{
    public class ELicencia_Software
    {
        public string id_licencia_administrador { get; set; }
        public string version_software { get; set; }
        public string nombre_licencia { get; set; }
        public string descripcion { get; set; }
        public string modalidad_pago { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_expiracion { get; set; }
        public bool estado_activacion { get; set; }
        public int cantidad_usuario { get; set; }
        public decimal precio_por_usuario { get; set; }
        public int cantidad_empresa { get; set; }
        public decimal precio_por_empresa { get; set; }
        public List<ELicencia_Software_Detalle> detalle_licencia { get; set; }
    }
}
