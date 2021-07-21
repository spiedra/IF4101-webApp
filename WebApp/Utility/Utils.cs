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

        public static List<CategoriaViewModel> ExcGetListCategorias(ConnectionDb connectionDb)
        {
            string commandText = "ADMINISTRACION.sp_GET_CATEGORIES";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.ExcecuteReader();
            return ReadGetListCategorias(connectionDb);
        }

        public static EstadiaViewModel ExcGetEstanciaByNombre(ConnectionDb connectionDb, string nombreEstancia)
        {
            string paramNombre = "@param_NOMBRE"
          , commandText = "ADMINISTRACION.sp_GET_ESTADIA_BY_NOMBRE";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramNombre, SqlDbType.VarChar, nombreEstancia);
            connectionDb.ExcecuteReader();
            return ReadGetEstadiaByCard(connectionDb);
        }

        public static List<EstadiaViewModel> ExcGetEstadias(ConnectionDb connectionDb)
        {
            string commandText = "ADMINISTRACION.sp_GET_ESTANCIAS";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.ExcecuteReader();
            return ReadGetEstancias(connectionDb);
        }

        public static void ExcDeleteEstancia(ConnectionDb connectionDb, int AppointmentId)
        {
            string paramEstanciaId = "@param_ID_ESTANCIA"
          , commandText = "ADMINISTRACION.sp_ELIMINAR_ESTADIA";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramEstanciaId, SqlDbType.Int, AppointmentId);
            connectionDb.ExecuteNonQuery();
        }

        public static void ExcDeleteCategoria(ConnectionDb connectionDb, CategoriaViewModel categoriaViewModel)
        {
            string paramEstanciaId = "@param_ID_CATEGORIA"
          , commandText = "ADMINISTRACION.sp_ELIMINAR_CATEGORIA";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramEstanciaId, SqlDbType.Int, categoriaViewModel.Id);
            connectionDb.ExecuteNonQuery();
        }

        private static EstadiaViewModel ReadGetEstadiaByCard(ConnectionDb connectionDb)
        {
            if (connectionDb.SqlDataReader.Read())
            {
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

        private static List<EstadiaViewModel> ReadGetEstancias(ConnectionDb connectionDb)
        {
            List<EstadiaViewModel> list = new();
            while (connectionDb.SqlDataReader.Read())
            {
                list.Add(new()
                {
                    ID = connectionDb.SqlDataReader.GetInt32(0),
                    Nombre = connectionDb.SqlDataReader.GetString(1),
                    Provincia = connectionDb.SqlDataReader.GetString(2),
                    Direccion = connectionDb.SqlDataReader.GetString(3),
                    PrecionNoche = connectionDb.SqlDataReader.GetDecimal(4),
                    Capacidad = connectionDb.SqlDataReader.GetInt32(5),
                    TipoCategoria = connectionDb.SqlDataReader.GetString(6),
                    Descripcion = connectionDb.SqlDataReader.GetString(7)
                });
            }
            connectionDb.SqlConnection.Close();
            return list;
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

        private static List<CategoriaViewModel> ReadGetListCategorias(ConnectionDb connectionDb)
        {
            List<CategoriaViewModel> list = new();
            while (connectionDb.SqlDataReader.Read())
            {
                list.Add(new CategoriaViewModel()
                {
                    Id = connectionDb.SqlDataReader.GetInt32(0),
                    Tipo = connectionDb.SqlDataReader.GetString(1)
                });

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

        public static bool ExcRegisterCategoria(ConnectionDb connectionDb, CategoriaViewModel categoriaViewModel)
        {
            string paramCategoriaTipo = "@param_TIPO"
           , commandText = "ADMINISTRACION.sp_REGISTER_CATEGORIA";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramCategoriaTipo, SqlDbType.VarChar, categoriaViewModel.Tipo);
            connectionDb.CreateParameterOutput();
            connectionDb.ExcecuteReader();
            return ReadParameterReturn(connectionDb);
        }

        public static bool ExcRegisterReserva(ConnectionDb connectionDb, int id_estancia, string nombrecompleto ,string cedula, string telefono, int cantidadPersonas, string fechaEntrada, string fechaSalida)
        {
            string paramIdEstancia = "@param_ID_ESTANCIA"
               , paramCedula = "@param_CEDULA_CLIENTE"
               , paramNombreCompleto = "@param_NOMBRE_COMPLETO"
               , paramCantidadPersonas = "@param_CANTIAD_PERSONAS"
               , paramTelefono = "@param_TELEFONO"
               , paramFechaEntrada = "@param_FECHA_ENTRADA"
               , paramFechaSalida = "@param_FECHA_SALIDA"
           , commandText = "[ADMINISTRACION].[sp_CONFIRMAR_RESERVA]";
            connectionDb.InitSqlComponents(commandText);
            connectionDb.CreateParameter(paramIdEstancia, SqlDbType.Int, id_estancia);
            connectionDb.CreateParameter(paramCedula, SqlDbType.VarChar, cedula);
            connectionDb.CreateParameter(paramNombreCompleto, SqlDbType.VarChar, nombrecompleto);
            connectionDb.CreateParameter(paramCantidadPersonas, SqlDbType.Int, cantidadPersonas);
            connectionDb.CreateParameter(paramTelefono, SqlDbType.VarChar, telefono);
            connectionDb.CreateParameter(paramFechaEntrada, SqlDbType.Date, fechaEntrada);
            connectionDb.CreateParameter(paramFechaSalida, SqlDbType.Date, fechaSalida);
            connectionDb.CreateParameterOutput();
            connectionDb.ExcecuteReader();
            return ReadParameterReturn(connectionDb);
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

        private static bool ReadParameterReturn(ConnectionDb connectionDb)
        {
            if ((int)connectionDb.ParameterReturn.Value == 1)
                return true;

            connectionDb.SqlConnection.Close();
            return false;
        }

    }
}
