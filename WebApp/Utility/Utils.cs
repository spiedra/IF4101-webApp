using WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApp.Utility
{
    public static class Utils
    {
        public static List<string> ExcGetList(ConnectionDb connectionDb)
        {
            string commandText = "ADMINISTRACION.sp_GET_TYPES_CATEGORIES";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.ExcecuteReader();
            return ReadGetList(connectionDb);
        }

        public static EstadiaViewModel ExcGetEstanciaByNombre(ConnectionDb connectionDb, string nombreEstancia)
        {
            string paramNombre = "@param_NOMBRE"
          , commandText = "ADMINISTRACION.sp_GET_ESTADIA_BY_NOMBRE ";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramNombre, SqlDbType.VarChar, nombreEstancia);
            connectionDb.ExcecuteReader();
            return ReadGetEstadiaByCard(connectionDb);
        }

        public static void ExcDeleteEstancia(ConnectionDb connectionDb, int AppointmentId)
        {
            string paramEstanciaId = "@param_ID_ESTANCIA"
          , commandText = "ADMINISTRACION.sp_ELIMINAR_ESTADIA";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramEstanciaId, SqlDbType.Int, AppointmentId);
            connectionDb.ExecuteNonQuery();
        }

        private static EstadiaViewModel ReadGetEstadiaByCard(ConnectionDb connectionDb)
        {
            if (connectionDb.SqlDataReader.Read()) { 
            EstadiaViewModel estadiaViewModel = new()
            {
                ID = connectionDb.SqlDataReader.GetInt32(0),
                Nombre = connectionDb.SqlDataReader.GetString(1),
                Provincia = connectionDb.SqlDataReader.GetString(2),
                Direccion = connectionDb.SqlDataReader.GetString(3),
                PrecionNoche = connectionDb.SqlDataReader.GetDecimal(4),
                Capacidad = connectionDb.SqlDataReader.GetInt32(5),
                TipoCategoria = connectionDb.SqlDataReader.GetString(6),
                Descripcion = connectionDb.SqlDataReader.GetString(7)
            };
                return estadiaViewModel;
            }
            connectionDb.SqlConnection.Close();

            return null;
        }

        private static List<string> ReadGetList(ConnectionDb connectionDb)
        {
            List<string> list = new();
            while (connectionDb.SqlDataReader.Read())
            {
                list.Add(connectionDb.SqlDataReader.GetString(0));
            }
            connectionDb.SqlConnection.Close();
            return list;
        }

        public static void ExcRegisterEstadia(ConnectionDb connectionDb, EstadiaViewModel appointmentViewModel)
        {
            string paramNombre = "@param_NOMBRE"
           , paramProvincia = "@param_PROVINCIA"
           , paraDireccion = "@param_DIRECCION"
           , paramPrecioNoche = "@param_PRECIO_NOCHE"
           , paramCapacidad = "@param_CAPACIDAD"
           //, paramRutaImagen = "@param_RUTA_IMAGEN"
           , paramTipoCategoria = "@param_TIPO_CATEGORIA"
           , paramDescripcion = "@param_DESCRIPCION"
           , commandText = "ADMINISTRACION.sp_REGISTRAR_ESTANDIAS";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramNombre, SqlDbType.VarChar, appointmentViewModel.Nombre);
            connectionDb.CreateParameter(paramProvincia, SqlDbType.VarChar, appointmentViewModel.Provincia);
            connectionDb.CreateParameter(paraDireccion, SqlDbType.VarChar, appointmentViewModel.Direccion);
            connectionDb.CreateParameter(paramPrecioNoche, SqlDbType.Decimal, appointmentViewModel.PrecionNoche);
            connectionDb.CreateParameter(paramCapacidad, SqlDbType.Int, appointmentViewModel.Capacidad);
            //connectionDb.CreateParameter(paramRutaImagen, SqlDbType.VarChar, appointmentViewModel.RutaImagen);
            connectionDb.CreateParameter(paramTipoCategoria, SqlDbType.VarChar, appointmentViewModel.TipoCategoria);
            connectionDb.CreateParameter(paramDescripcion, SqlDbType.VarChar, appointmentViewModel.Descripcion);
            connectionDb.ExecuteNonQuery();
        }

        public static void ExcActualizarEstadia(ConnectionDb connectionDb, EstadiaViewModel estadiaVieModel)
        {
            string paramIdEstadia = "@param_ID"
           , paramNombre = "@param_NOMBRE"
           , paramProvincia = "@param_PROVINCIA"
           , paraDireccion = "@param_DIRECCION"
           , paramPrecioNoche = "@param_PRECIO_NOCHE"
           , paramCapacidad = "@param_CAPACIDAD"
           , paramTipoCategoria = "@param_TIPO_CATEGORIA"
           , paramDescripcion = "@param_DESCRIPCION"
           , commandText = "ADMINISTRACION.sp_UPDATE_ESTANCIA";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramIdEstadia, SqlDbType.Int, estadiaVieModel.ID);
            connectionDb.CreateParameter(paramNombre, SqlDbType.VarChar, estadiaVieModel.Nombre);
            connectionDb.CreateParameter(paramProvincia, SqlDbType.VarChar, estadiaVieModel.Provincia);
            connectionDb.CreateParameter(paraDireccion, SqlDbType.VarChar, estadiaVieModel.Direccion);
            connectionDb.CreateParameter(paramPrecioNoche, SqlDbType.Decimal, estadiaVieModel.PrecionNoche);
            connectionDb.CreateParameter(paramCapacidad, SqlDbType.Int, estadiaVieModel.Capacidad);
            connectionDb.CreateParameter(paramTipoCategoria, SqlDbType.VarChar, estadiaVieModel.TipoCategoria);
            connectionDb.CreateParameter(paramDescripcion, SqlDbType.VarChar, estadiaVieModel.Descripcion);
            connectionDb.ExecuteNonQuery();
        }

        public static void GuardarImagen(IFormFile file)
        {
            if (file != null)
            {
                using FileStream fs = new FileStream("../wwwroot", FileMode.Create);
                file.CopyTo(fs);
            }
        }
    }
}
