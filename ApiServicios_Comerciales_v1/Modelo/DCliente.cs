using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServicios_Comerciales_v1.Entidad;
using Microsoft.Data.SqlClient;

namespace ApiServicios_Comerciales_v1.Modelo
{
    public class DCliente
    {
        public string res()
        {
            string res = "";
            using (var con = new SqlConnection(new Conexion().cn()))
            {
                con.Open();
                res = "Conexion abierta";
            }
            return res;
        }
    }
}
