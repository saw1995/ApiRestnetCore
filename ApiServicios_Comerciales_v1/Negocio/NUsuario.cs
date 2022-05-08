using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServicios_Comerciales_v1.Modelo;
using ApiServicios_Comerciales_v1.Entidad;
using System.ComponentModel.DataAnnotations;

namespace ApiServicios_Comerciales_v1.Negocio
{
    public class NUsuario
    {
        public async Task<ERespuesta> registrar_usuario_cliente(
            string _email, string _nombre, string _apellido_paterno, string _apellido_materno, string _contrasenna)
        {
            var res = new ERespuesta();
            res.estado = 0; res.nombre = "Validación";
            res.codigo = "1002";

            if(_email == null || Convert.ToString(_email) == "")
            {
                res.mensaje = "Error falta de parametro email";
            }
            else if(_nombre == null || Convert.ToString(_nombre) == "")
            {
                res.mensaje = "Error falta de parametro nombre";
            }
            else if(_apellido_paterno == null || Convert.ToString(_apellido_paterno) == "")
            {
                res.mensaje = "Error falta de parametro apellido_paterno";
            }
            else if(_contrasenna == null || Convert.ToString(_contrasenna) == "")
            {
                res.mensaje = "Error falta de parametro contrasenna";
            }
            else
            {
                var busquedaValor = await new DUsuario().busqueda_usuario_by_email(Convert.ToString(_email).Trim());

                if(busquedaValor.estado == 1)
                {
                    res = busquedaValor;
                    res.codigo = "1003";
                    res.mensaje = busquedaValor.mensaje;
                    res.nombre = "Duplicado";
                    res.estado = 0;
                }
                else
                {
                    string id_usuario_rol = new zAuxiliares().generarId();

                    string id_usuario = new zAuxiliares().generarId();

                    string id_licencia = new zAuxiliares().generarId();

                    DateTime fecha = DateTime.Now;

                    EUsuario oUsuario = new EUsuario { id_usuario = id_usuario,
                    id_usuario_rol = id_usuario_rol,
                    usuario_administrador = true,
                    ci = "",
                    nombre = Convert.ToString(_nombre),
                    apellido_paterno = Convert.ToString(_apellido_paterno),
                    apellido_materno = Convert.ToString(_apellido_materno),
                    email = Convert.ToString(_email),
                    telefono = "",
                    celular = "",
                    foto = "sin imagen",
                    estado = true
                    };

                    List<Task<ERespuesta>> listaTareas = new List<Task<ERespuesta>>();
                    listaTareas.Add(new DUsuario().insertar_usuario(oUsuario, _contrasenna, id_licencia));

                    listaTareas.Add(new DUsuario().insertar_usuario_rol(
                            id_usuario_rol, "Administrador", id_licencia
                        ));

                    var resultadoTareas = await Task.WhenAll(listaTareas);

                    bool validarTareas = true;
                    string mensajeError = "Se registro el usuario con exito.";
                    for(int i = 0; i <= resultadoTareas.Length - 1; i++)
                    {
                        if( resultadoTareas[i].estado == 0)
                        {
                            validarTareas = false;
                            mensajeError = resultadoTareas[i].mensaje;
                            i = resultadoTareas.Length - 1;
                        }
                    }

                    if(validarTareas)
                    {
                        res.estado = 1; res.codigo = "10056";
                        res.nombre = "Registro Nuevo"; res.mensaje = mensajeError;
                    }
                    else
                    {
                        res.estado = 0; res.codigo = "10057";
                        res.nombre = "Registro Nuevo"; res.mensaje = mensajeError;
                    }
                }
            }

            return res;
        }

        public async Task<EUsuario_Licencia_login> login_usuario_by_ci_email(string _texto, string _contrasenna)
        {
            EUsuario_Licencia_login res = new EUsuario_Licencia_login();
            res.estado = 0;
            res.mensaje = "no se ejecuto correctamente. . .";

            if(_texto == null || Convert.ToString(_texto) == "")
            {
                res.mensaje = "Error en el parametro de texto o es vacío. . .";
            }
            else if(_contrasenna == null || Convert.ToString(_contrasenna) == "")
            {
                res.mensaje = "Error en el parametro Contrasenna, o es valor nulo";
            }
            else
            {
                var resultQuery = await new DUsuario().busqueda_usuario_by_email_ci(_texto, _contrasenna);

                if(resultQuery.estado == 1)
                {
                    res = resultQuery;
                    res.licencia_software.detalle_licencia = await new DUsuario().lista_licencia_detalle_by_id_licencia(res.licencia_software.id_licencia_administrador);
                }
                else
                {
                    res.mensaje = "usuario no registrado en el sistema. . .";
                }
            }
            return res;
        }

        //hasta aqui el codigo net de c#
    }
}
