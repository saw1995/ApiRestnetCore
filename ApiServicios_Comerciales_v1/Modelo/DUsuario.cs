using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ApiServicios_Comerciales_v1.Entidad;

namespace ApiServicios_Comerciales_v1.Modelo
{
    public class DUsuario
    {
        //metodos de insertado
        public async Task<ERespuesta> insertar_usuario(EUsuario obj, string contrasenna, string id_licencia_administrador)
        {
            ERespuesta res = new ERespuesta();
            res.estado = 0; res.mensaje = "No se agrego el registro usuario.";
            res.codigo = "1001"; res.nombre = "SQL";

            using(var con = new SqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "INSERT INTO usuario(id,id_usuario_rol, usuario_administrador, id_licencia_administrador, "
                     + "ci, nombre, apellido_paterno, apellido_materno, email, telefono, celular, foto, contrasenna, estado) "
                     + "VALUES(@id,@id_usuario_rol,@usuario_administrador,@id_licencia_administrador, "
                     + "@ci,@nombre,@appaterno,@apmaterno,@email,@telefono,@celular,@foto,@contrasenna,1)";

                using(var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", obj.id_usuario);
                    cmd.Parameters.AddWithValue("@id_usuario_rol", obj.id_usuario_rol);
                    cmd.Parameters.AddWithValue("@usuario_administrador", obj.usuario_administrador);
                    cmd.Parameters.AddWithValue("@id_licencia_administrador", id_licencia_administrador);
                    cmd.Parameters.AddWithValue("@ci", obj.ci);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@appaterno", obj.apellido_paterno);
                    cmd.Parameters.AddWithValue("@apmaterno", obj.apellido_materno);
                    cmd.Parameters.AddWithValue("@email", obj.email);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@celular", obj.celular);
                    cmd.Parameters.AddWithValue("@foto", obj.foto);
                    cmd.Parameters.AddWithValue("@contrasenna", contrasenna);

                    try
                    {
                        int valor = await cmd.ExecuteNonQueryAsync();

                        if(valor == 1)
                        {
                            res.estado = 1; res.mensaje = "Se guardo con exito el registro.";
                        }
                    }
                    catch (Exception ex)
                    {
                        res.estado = 0;
                        res.mensaje = "usuario: " + ex.Message.ToString();
                        res.codigo = "1001";
                        res.nombre = "SQL";
                    }
                }
            }
            return res;
        }

        public async Task<ERespuesta> insertar_usuario_rol(string id, string nombre, string licencia_adm)
        {
            ERespuesta res = new ERespuesta();
            res.codigo = "1001"; res.estado = 0;
            res.mensaje = "No se agrego la consulta en Usuario_rol"; res.nombre = "SQL";

            using(var con = new SqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "INSERT INTO usuario_rol(id, nombre, id_licencia_administrador,estado) VALUES(@id,@nombre,@licencia,1)";

                using(var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@licencia", licencia_adm);

                    try
                    {
                        int i = await cmd.ExecuteNonQueryAsync();

                        if (i == 1)
                        {
                            res.estado = 1;
                            res.mensaje = "Se guardo el registro.";
                        }
                    }
                    catch (Exception ex)
                    {
                        res.mensaje = "usuario_rol: " + ex.Message.ToString();
                    }
                }
            }

            return res;
        }

        //metodos de busqueda y seleccion
        public async Task<ERespuesta> busqueda_usuario_by_email(string texto)
        {
            ERespuesta res = new ERespuesta();
            res.estado = 0; res.mensaje = "sin accion";
            res.codigo = "1001"; res.nombre = "SQL";
            
            using(var con = new SqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();
                string sql = "SELECT TOP 1 id FROM usuario WHERE email=@texto ";

                using(var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@texto", texto);
                    try
                    {
                        var valor = await cmd.ExecuteScalarAsync();
                        if (Convert.ToString(valor) != "")
                        {
                            res.estado = 1;
                            res.mensaje = "El email o Ci ya se encuentra registrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        res.mensaje = ex.Message.ToString();
                    }
                }
            }
            return res;
        }

        //inicio de sesion usuario...
        public async Task<EUsuario_Licencia_login> busqueda_usuario_by_email_ci(string texto, string contrasenna)
        {
            EUsuario_Licencia_login res = new EUsuario_Licencia_login();
            res.estado = 0;
            res.mensaje = "no se ejecuto correctamente. . .";

            using (var con = new SqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();
                string sql = "SELECT usuario.id as 'id_usuario', usuario.ci, usuario.nombre, usuario.apellido_paterno,usuario.apellido_materno, "
                + "usuario.email, usuario.telefono, usuario.celular, usuario.foto, usuario.estado, "
                + "usuario_rol.id as 'id_rol', usuario_rol.nombre as 'nombre_rol',"
                + "licencia_administrador.id as 'id_licencia', licencia_administrador.software_version, "
                + "licencia_administrador.nombre as 'nombre_licencia', licencia_administrador.descripcion as 'descripcion_software', "
                + "licencia_administrador.modalidad_pago, licencia_administrador.fecha_inicio, licencia_administrador.fecha_expiracion, "
                + "licencia_administrador.estado_activado, licencia_administrador.cantidad_usuario, licencia_administrador.precio_usuario, "
                + "licencia_administrador.cantidad_empresa, licencia_administrador.precio_empresa "
                + "from usuario "
                + "inner join licencia_administrador on usuario.id_licencia_administrador = licencia_administrador.id "
                + "inner join usuario_rol on usuario.id_usuario_rol = usuario_rol.id "
                + "WHERE (usuario.ci = @texto and usuario.contrasenna = @contrasenna) or (usuario.email = @texto and usuario.contrasenna = @contrasenna)";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@texto", texto);
                    cmd.Parameters.AddWithValue("@contrasenna", contrasenna);
                    try
                    {
                        using(var drd = await cmd.ExecuteReaderAsync())
                        {
                            if(drd.HasRows)
                            {
                                res.estado = 1;
                                res.mensaje = "Se encontraron registros. . .";

                                var oUsuario = new EUsuario();
                                var oLicencia = new ELicencia_Software();

                                while(await drd.ReadAsync())
                                {
                                    oUsuario.id_usuario = Convert.ToString(drd["id_usuario"]);
                                    oUsuario.ci = Convert.ToString(drd["ci"]);
                                    oUsuario.nombre = Convert.ToString(drd["nombre"]);
                                    oUsuario.apellido_paterno = Convert.ToString(drd["apellido_paterno"]);
                                    oUsuario.apellido_materno = Convert.ToString(drd["apellido_materno"]);
                                    oUsuario.email = Convert.ToString(drd["email"]);
                                    oUsuario.telefono = Convert.ToString(drd["telefono"]);
                                    oUsuario.celular = Convert.ToString(drd["celular"]);
                                    oUsuario.foto = Convert.ToString(drd["foto"]);
                                    oUsuario.estado = Convert.ToBoolean(drd["estado"]);
                                    oUsuario.id_usuario_rol = Convert.ToString(drd["id_rol"]) ;
                                    oUsuario.nombre_rol = Convert.ToString(drd["nombre_rol"]);

                                    oLicencia.id_licencia_administrador = Convert.ToString(drd["id_licencia"]);
                                    oLicencia.version_software = Convert.ToString(drd["software_version"]);
                                    oLicencia.nombre_licencia = Convert.ToString(drd["nombre_licencia"]);
                                    oLicencia.descripcion = Convert.ToString(drd["descripcion_software"]);
                                    oLicencia.modalidad_pago = Convert.ToString(drd["modalidad_pago"]);
                                    oLicencia.fecha_inicio = Convert.ToDateTime(drd["fecha_inicio"]);
                                    oLicencia.fecha_expiracion = Convert.ToDateTime(drd["fecha_expiracion"]);
                                    oLicencia.estado_activacion = Convert.ToBoolean(drd["estado_activado"]);
                                    oLicencia.cantidad_usuario = Convert.ToString(drd["cantidad_usuario"]) != "" ?
                                        Convert.ToInt32(drd["cantidad_usuario"]) : 0;
                                    oLicencia.precio_por_usuario = Convert.ToString(drd["precio_usuario"]) != "" ?
                                        Convert.ToDecimal(drd["precio_usuario"]) : 0;
                                    oLicencia.cantidad_empresa = Convert.ToString(drd["cantidad_empresa"]) != "" ?
                                        Convert.ToInt32(drd["cantidad_empresa"]) : 0;
                                    oLicencia.precio_por_empresa = Convert.ToString(drd["precio_empresa"]) != "" ?
                                        Convert.ToDecimal(drd["precio_empresa"]) : 0;
                                }

                                res.usuario = oUsuario;
                                res.licencia_software = oLicencia;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        res.mensaje = ex.Message.ToString();
                    }
                }
            }
            return res;
        }

        //lista de licencia detalle
        public async Task<List<ELicencia_Software_Detalle>> lista_licencia_detalle_by_id_licencia(string id_licencia)
        {
            List<ELicencia_Software_Detalle> res = new List<ELicencia_Software_Detalle>();
            
            using(var con = new SqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT licencia_administrador_detalle.id, modulo.id as 'id_modulo', modulo.nombre, modulo.detalle, licencia_administrador_detalle.precio FROM "
                    + "licencia_administrador_detalle inner join modulo on modulo.id = licencia_administrador_detalle.id_modulo "
                    + "WHERE licencia_administrador_detalle.id_licencia_administrador = @id_licencia";

                using(var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_licencia", id_licencia);

                    using(var drd = await cmd.ExecuteReaderAsync())
                    {
                        if(drd.HasRows)
                        {
                            while(await drd.ReadAsync())
                            {
                                res.Add(new ELicencia_Software_Detalle{ 
                                    id_licencia_detalle = Convert.ToString(drd["id"]),
                                    id_modulo = Convert.ToString(drd["id_modulo"]),
                                    nombre = Convert.ToString(drd["nombre"]),
                                    detalle = Convert.ToString(drd["detalle"]),
                                    precio = Convert.ToString(drd["precio"]) == "" ? 0 : Convert.ToDecimal(drd["precio"])
                                });
                            }
                        }
                    }
                }
            }
            return res;
        }

        //hasta aqui el codigo net de C#
    }
}
