using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServicios_Comerciales_v1
{
    public class zAuxiliares
    {
        public string generarId()
        {
            string id = "";

            Random random = new Random();
            int a = random.Next(1, 999);
            int b = random.Next(1, 999);
            int c = random.Next(1, 999);

            DateTime fecha = DateTime.Now;

            id = fecha.ToString("dd-mm-yyyy") + "-" + a.ToString("000") + "-" + b.ToString("000") + "-" +  c.ToString("000");

            return id;
        }
    }
}
