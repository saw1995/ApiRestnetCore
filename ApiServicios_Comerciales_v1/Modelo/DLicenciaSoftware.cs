using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ApiServicios_Comerciales_v1.Entidad;

namespace ApiServicios_Comerciales_v1.Modelo
{
    public class DLicenciaSoftware
    {
        public async Task<ERespuesta> insertar_licencia_administrador(string id, string id_usuario, string software, string nombre, string descripcion,
            string modalidad_pago, DateTime fecha_inicio, DateTime fecha_expiracion, bool activacion, bool estado)
        {
            ERespuesta oRespuesta = new ERespuesta();
            oRespuesta.estado = 0; oRespuesta.codigo = "1001";
            oRespuesta.nombre = "SQL"; oRespuesta.mensaje = "No se ejecuto la consulta sql en licencia_administrador";

            using(var con = new SqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "INSERT INTO licencia_administrador(id, id_usuario, software_version, nombre, descripcion, "
                    + "modalidad_pago, fecha_inicio, fecha_expiracion, estado_activado, estado) "
                    + "VALUES(@id, @id_usuario, @software, @nombre, @descripcion, "
                    + "@modalidad_pago, @fecha_inicio, @fecha_exp, @activacion, @estado)";

                using(var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@id_usuario", id_usuario);
                    cmd.Parameters.AddWithValue("@software", software);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@modalidad_pago", modalidad_pago);
                    cmd.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                    cmd.Parameters.AddWithValue("@fecha_exp", fecha_expiracion);
                    cmd.Parameters.AddWithValue("@activacion", activacion);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    try
                    {
                        int i = await cmd.ExecuteNonQueryAsync();

                        if(i == 1)
                        {
                            oRespuesta.estado = 1;
                            oRespuesta.mensaje = "Registro guardado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        oRespuesta.mensaje = "licencia_sofotware: " + ex.Message.ToString();
                    }
                }
            }
            return oRespuesta;
        }


        //metodo Update
        public async Task<ERespuesta> actualizar_licencia_administrador(string id, string id_usuario, string software, string nombre, string descripcion,
            string modalidad_pago, DateTime fecha_inicio, DateTime fecha_expiracion, bool activacion, bool estado)
        {
            ERespuesta oRespuesta = new ERespuesta();
            oRespuesta.estado = 0; oRespuesta.codigo = "1001";
            oRespuesta.nombre = "SQL"; oRespuesta.mensaje = "No se ejecuto la consulta sql en licencia_administrador";

            using (var con = new SqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "INSERT INTO licencia_administrador(id, id_usuario, software_version, nombre, descripcion, "
                    + "modalidad_pago, fecha_inicio, fecha_expiracion, estado_activado, estado) "
                    + "VALUES(@id, @id_usuario, @software, @nombre, @descripcion, "
                    + "@modalidad_pago, @fecha_inicio, @fecha_exp, @activacion, @estado)";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@id_usuario", id_usuario);
                    cmd.Parameters.AddWithValue("@software", software);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@modalidad_pago", modalidad_pago);
                    cmd.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                    cmd.Parameters.AddWithValue("@fecha_exp", fecha_expiracion);
                    cmd.Parameters.AddWithValue("@activacion", activacion);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    try
                    {
                        int i = await cmd.ExecuteNonQueryAsync();

                        if (i == 1)
                        {
                            oRespuesta.estado = 1;
                            oRespuesta.mensaje = "Registro guardado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        oRespuesta.mensaje = "licencia_sofotware: " + ex.Message.ToString();
                    }
                }
            }
            return oRespuesta;
        }
    }
}
