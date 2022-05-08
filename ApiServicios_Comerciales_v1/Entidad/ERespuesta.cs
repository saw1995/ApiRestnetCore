using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServicios_Comerciales_v1.Entidad
{
    public class ERespuesta
    {
        public int estado { get; set; }
        public string mensaje { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public object datos { get; set; }
    }
}
