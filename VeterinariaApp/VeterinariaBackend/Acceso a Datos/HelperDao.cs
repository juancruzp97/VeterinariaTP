using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinariaBackend.Dominio;

namespace VeterinariaBackend.Acceso_a_Datos
{
    class HelperDao
    {
        private static HelperDao instancia;
        private string conexionString;

        private HelperDao()
        {
            conexionString = @"Data Source=localhost;Initial Catalog=db_Veterinaria;Integrated Security=True";
        }

        public static HelperDao GetInstance()
        {
            if (instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }

        //                             MASCOTA

        //INSERT
        public bool InsertarMascota(string spAltaM, Mascota mascota, int cod)
        {
            bool flag = true;

            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();

                SqlCommand cmd = new SqlCommand(spAltaM, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nommascota", mascota.Nombre);
                cmd.Parameters.AddWithValue("@edad", mascota.Edad);
                cmd.Parameters.AddWithValue("@tipo", mascota.TipoMascota);
                cmd.Parameters.AddWithValue("@cliente", cod);

                cmd.ExecuteNonQuery();
                transaccion.Commit();


            }
            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;

            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;
        }
        public bool InsertarSql(Mascota oMascota, string spMascota, string spAtencion, int id)
        {
            bool flag = true;

            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;


            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();

                SqlCommand cmd = new SqlCommand(spMascota, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nommascota", oMascota.Nombre);
                cmd.Parameters.AddWithValue("@edad", oMascota.Edad);
                cmd.Parameters.AddWithValue("@tipo", oMascota.TipoMascota);
                cmd.Parameters.AddWithValue("@cliente", id);

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@cod_mascota";
                param.SqlDbType = SqlDbType.Int;

                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int codMascota = (int)param.Value;
                oMascota.CodigoMascota = codMascota;
                //int codMascota = 15;
                //int nroAtencion = 0;
                //transaction.Commit();


                foreach (Atencion aten in oMascota.ListaAtencion)
                {

                    SqlCommand cmd2 = new SqlCommand(spAtencion, cnn);

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Transaction = transaccion;
                    cmd2.Parameters.AddWithValue("@cod_atencion", aten.CodAtencion);
                    cmd2.Parameters.AddWithValue("@cod_mascota", oMascota.CodigoMascota);
                    cmd2.Parameters.AddWithValue("@fecha", aten.Fecha);
                    cmd2.Parameters.AddWithValue("@descripcion", aten.Descripcion);
                    cmd2.Parameters.AddWithValue("@importe", aten.Importe);
                    cmd2.ExecuteNonQuery();
                }

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();


                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;


        }

        //SELECT
        public int GetIdMascota(string spId, int cliente, string nombre)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            int id = 0;
            try
            {

                cnn.Open();
                SqlCommand cmd = new SqlCommand(spId, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cliente", cliente);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@cod";
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                id = (int)param.Value;

            }
            catch
            {
                return id;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return id;

        }      
        public List<Mascota> ConsultarMascotaCliente(string spMasCliente, int cod)
        {
            List<Mascota> lista = new List<Mascota>();
            SqlConnection cnn = new SqlConnection(conexionString);
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(spMasCliente, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod", cod);

                DataTable tabla = new DataTable();
                tabla.Load(cmd.ExecuteReader());

                cnn.Close();


                foreach (DataRow row in tabla.Rows)
                {
                    Mascota mascota = new Mascota();
                    mascota.CodigoMascota = Convert.ToInt32(row["cod_mascota"].ToString());
                    mascota.Nombre = row["nombre"].ToString();
                    mascota.Edad = Convert.ToInt32(row["edad"].ToString());
                    mascota.TipoMascota = Convert.ToInt32(row["tipo"].ToString());
                    lista.Add(mascota);
                }
            }
            catch
            {
                return lista;
            }

            return lista;
        }
        public DataTable MascotaPorNombre(string sP,string nombre)
        {
            // List<Mascota> lista = new List<Mascota>();
            SqlConnection cnn = new SqlConnection(conexionString);

            cnn.Open();
            SqlCommand cmd = new SqlCommand(sP, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nom", nombre);

            DataTable tabla = new DataTable();
            tabla.Load(cmd.ExecuteReader());

            cnn.Close();
            return tabla;
        }

        //DELETE
        public bool DeleteMascota(string sP,int idMascota)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;
            bool flag = true;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_DELETE_MASCOTA", cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idmascota", idMascota);
                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;
        }

        //UPDATE
        public bool UpdateMascota(string spUpdate, Mascota mascota)
        {
            bool flag = true;
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(spUpdate, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod", mascota.CodigoMascota);
                cmd.Parameters.AddWithValue("@nom", mascota.Nombre);
                cmd.Parameters.AddWithValue("@edad", mascota.Edad);
                cmd.Parameters.AddWithValue("@tipo", mascota.TipoMascota);

                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;

        }


        //                             ATENCION

        //SELECT
        public List<int> GetIdAtencion(string sP,int idMascota)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            List<int> detalle = new List<int>();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sP, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mascota", idMascota);
                DataTable tabla = new DataTable();
                tabla.Load(cmd.ExecuteReader());

                foreach (DataRow fila in tabla.Rows)
                {
                    int i = Convert.ToInt32(fila["cod_atencion"].ToString());
                    detalle.Add(i);
                }

            }
            catch (SqlException)
            {
                return detalle;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return detalle;

        }
        public int ProximoDetalle(string sP,int idMascota)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            int detalle = 0;
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sP, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cod", idMascota);
                SqlParameter param = new SqlParameter();
                param.Direction = ParameterDirection.Output;
                param.ParameterName = "@nro";
                param.SqlDbType = SqlDbType.Int;

                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                detalle = (int)param.Value;

            }
            catch
            {
                detalle = 0;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return detalle;
        }
        public List<Atencion> ObtenerAtencion(string spAtencion, int cod)
        {
            List<Atencion> lista = new List<Atencion>();
            SqlConnection cnn = new SqlConnection(conexionString);

            cnn.Open();
            SqlCommand cmd = new SqlCommand(spAtencion, cnn);
            DataTable tabla = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cod", cod);
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            foreach (DataRow filas in tabla.Rows)
            {
                Atencion oAtencion = new Atencion();
                oAtencion.CodAtencion = Convert.ToInt32(filas["cod_atencion"].ToString());
                oAtencion.Fecha = Convert.ToDateTime(filas["fecha"].ToString());
                oAtencion.Descripcion = filas["descripcion"].ToString();
                oAtencion.Importe = Convert.ToDouble(filas["importe"].ToString());

                lista.Add(oAtencion);
            }

            return lista;
        }

        //INSERT
        public bool InsertarAtencion(string sP,Mascota oMascota)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;
            bool flag = true;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();

                for (int i = 0; i < oMascota.ListaAtencion.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand(sP, cnn, transaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_atencion", oMascota.ListaAtencion[i].CodAtencion);
                    cmd.Parameters.AddWithValue("@cod_mascota", oMascota.CodigoMascota);
                    cmd.Parameters.AddWithValue("@fecha", oMascota.ListaAtencion[i].Fecha);
                    cmd.Parameters.AddWithValue("@descripcion", oMascota.ListaAtencion[i].Descripcion);
                    cmd.Parameters.AddWithValue("@importe", oMascota.ListaAtencion[i].Importe);
                    cmd.ExecuteNonQuery();
                }

                transaccion.Commit();
            }
            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;
        }
        public bool InsertarDetalleAtencion(List<Atencion> atencion,int idM, string spDetalleA)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;
            bool flag = true;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();

                for (int i = 0; i < atencion.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand(spDetalleA, cnn, transaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_atencion", atencion[i].CodAtencion);
                    cmd.Parameters.AddWithValue("@cod_mascota",idM);
                    cmd.Parameters.AddWithValue("@fecha", atencion[i].Fecha);
                    cmd.Parameters.AddWithValue("@descripcion",atencion[i].Descripcion);
                    cmd.Parameters.AddWithValue("@importe",atencion[i].Importe);

                    cmd.ExecuteNonQuery();
                }

                transaccion.Commit();
             
            }
            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;

            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return flag;
        }


        //DELETE
        public bool DeleteAtencion(string sP,int idMascota)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;
            bool flag = true;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(sP, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codmascota", idMascota);
                cmd.ExecuteNonQuery();

                transaccion.Commit();

            }
            catch (Exception)
            {

                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;
        }
        public bool DeleteDetalleAtencion(string sP,int idMascota, int idDetalle)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;
            bool flag = true;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(sP, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atencion", idDetalle);
                cmd.Parameters.AddWithValue("@mascota", idMascota);
                cmd.ExecuteNonQuery();

                transaccion.Commit();

            }
            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;

            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;
        }

        //UPDATE
        public bool UpdateAtencion(string spUpAt, Atencion atencion, int id)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;
            bool flag = true;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(spUpAt, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atencion", atencion.CodAtencion);
                cmd.Parameters.AddWithValue("@mascota", id);
                cmd.Parameters.AddWithValue("@fecha", atencion.Fecha);
                cmd.Parameters.AddWithValue("@descripcion", atencion.Descripcion);
                cmd.Parameters.AddWithValue("@importe", atencion.Importe);
                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;

            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return flag;
        }


        //                          CLIENTES
        public List<Clientes> ObtenerClientes(string spCliente)
        {
            List<Clientes> lista = new List<Clientes>();
            SqlConnection cnn = new SqlConnection(conexionString);

            cnn.Open();
            SqlCommand cmd = new SqlCommand(spCliente, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable tabla = new DataTable();
            tabla.Load(cmd.ExecuteReader());

            cnn.Close();

            foreach (DataRow row in tabla.Rows)
            {
                Clientes cliente = new Clientes();
                cliente.Codigo = Convert.ToInt32(row["cod_cliente"].ToString());
                cliente.Nombre = row["nombre"].ToString();
                if (row["sexo"].ToString().Equals("M"))
                {
                    cliente.Sexo = true;
                }
                else
                {
                    cliente.Sexo = false;
                }
                cliente.Direccion = row["direccion"].ToString();
                cliente.Documento = Convert.ToInt32(row["documento"].ToString());
                cliente.Telefono = Convert.ToInt32(row["telefono"].ToString());
                cliente.Edad = Convert.ToInt32(row["edad"].ToString());
                

                lista.Add(cliente);
            }

            return lista;

        }
        public bool InsertarCliente(string spAltaC,Clientes cliente)
        {
            bool flag = true;
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(spAltaC, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                if (cliente.Sexo == true)
                {
                    cmd.Parameters.AddWithValue("@sexo", 'M');
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sexo", 'F');
                }
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@edad", cliente.Edad);

                cmd.ExecuteNonQuery();

                transaccion.Commit();

            }

            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;
        }

        //UPDATE

        public bool UpdateCliente(string spUpC,Clientes cliente)
        {
            bool flag = true;
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(spUpC, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre",cliente.Nombre);
                if(cliente.Sexo == true)
                {
                    cmd.Parameters.AddWithValue("@sexo",'M');
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sexo",'F');
                }
                cmd.Parameters.AddWithValue("@telefono",cliente.Telefono);
                cmd.Parameters.AddWithValue("@documento",cliente.Documento);
                cmd.Parameters.AddWithValue("@direccion",cliente.Direccion);
                cmd.Parameters.AddWithValue("@edad",cliente.Edad);
                cmd.Parameters.AddWithValue("@codigo",cliente.Codigo);

                cmd.ExecuteNonQuery();

                transaccion.Commit();


        }
            catch (Exception)
            {
                flag = false;
                transaccion.Rollback();

            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return flag;
        }

        //DELETE

        public bool DeleteCliente(string spDelC,int id)
        {
            bool flag = true;
            SqlConnection cnn = new SqlConnection(conexionString);
            SqlTransaction transaccion = null;

            try
            {
                cnn.Open();
                transaccion = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(spDelC, cnn, transaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", id);
                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch (Exception)
            {
                transaccion.Rollback();
                flag = false;

            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return flag;
        }


        //LOGIN
        public bool LoginCheck(string sP,string user, string pass)
        {
            SqlConnection cnn = new SqlConnection(conexionString);
            bool flag = true;

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sP, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    flag = false;
                }


                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }

            }
            return flag;
        }










    }
}
