using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MVC_ECommerce.Models
{
    public class AdministracionProducto
    {
        private SqlConnection con;

        private void Conectar()
        {
            //Crea una conexion a la base de datos
            string constr = "Data Source = DESKTOP-PH3T3KJ\\SQLEXPRESS; Initial Catalog = ECommerce; Integrated Security = True";
            con = new SqlConnection(constr);
        }
        public int AltaProducto(Contacto contacto, Empresa empresa, Producto producto)
        {
            Conectar();
            int id = 0;
            //Comprueba que el contacto tiene datos, si no los tiene, busca el nombre de la empresa en la base de datos y utiliza esto.
            if (contacto.Mail == null)
            {
                //Obtiene el ID de la Empresa existente
                SqlCommand comandoIDEmpresa = new SqlCommand("select ID from Empresa where Nombre = @Nombre", con);
                comandoIDEmpresa.Parameters.Add("@Nombre", SqlDbType.VarChar);
                comandoIDEmpresa.Parameters["@Nombre"].Value = empresa.Nombre;
                //Ejecuta el reader del comando anterior y asigna el valor del ID encontrado a la variable usada anteriormente
                con.Open();
                SqlDataReader idEmpresa = comandoIDEmpresa.ExecuteReader();
                while (idEmpresa.Read())
                {
                    id = Convert.ToInt32(idEmpresa[0]);
                }
                idEmpresa.Close();
                con.Close();
                //Crea un producto utilizando el ID de Empresa recien obtenido
                var comandoProducto = new SqlCommand("insert into Producto(ID_Empresa, Stock, Precio_Costo, Margen, Precio_Bruto, Precio_Neto, Nombre, Detalle, Tipo_Componente, Marca) values (@ID_Empresa, @Stock, @Precio_Costo, @Margen, @Precio_Bruto, @Precio_Neto, @Nombre, @Detalle, @Tipo_Componente, @Marca)", con);
                comandoProducto.Parameters.Add("@ID_Empresa", SqlDbType.Int);
                comandoProducto.Parameters.Add("@Stock", SqlDbType.Int);
                comandoProducto.Parameters.Add("@Precio_Costo", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Margen", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Precio_Bruto", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Precio_Neto", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Nombre", SqlDbType.VarChar);
                comandoProducto.Parameters.Add("@Detalle", SqlDbType.VarChar);
                comandoProducto.Parameters.Add("@Tipo_Componente", SqlDbType.VarChar);
                comandoProducto.Parameters.Add("@Marca", SqlDbType.VarChar);
                comandoProducto.Parameters["@ID_Empresa"].Value = id;
                comandoProducto.Parameters["@Stock"].Value = producto.Stock;
                comandoProducto.Parameters["@Precio_Costo"].Value = producto.PrecioCosto;
                comandoProducto.Parameters["@Margen"].Value = producto.Margen;
                comandoProducto.Parameters["@Precio_Bruto"].Value = producto.PrecioBruto;
                comandoProducto.Parameters["@Precio_Neto"].Value = producto.PrecioNeto;
                comandoProducto.Parameters["@Nombre"].Value = producto.Nombre;
                comandoProducto.Parameters["@Detalle"].Value = producto.Detalle;
                comandoProducto.Parameters["@Tipo_Componente"].Value = producto.TipoComponente;
                comandoProducto.Parameters["@Marca"].Value = producto.Marca;

                con.Open();
                int i = comandoProducto.ExecuteNonQuery();
                con.Close();

                return i;
            }
            else
            {
                //Crea un comando para insertar un contacto
                SqlCommand comandoContactoNuevo = new SqlCommand("insert into Contacto(Mail,Direccion,Telefono) values (@mail,@direccion,@telefono)", con);
                comandoContactoNuevo.Parameters.Add("@mail", SqlDbType.VarChar);
                comandoContactoNuevo.Parameters.Add("@direccion", SqlDbType.VarChar);
                comandoContactoNuevo.Parameters.Add("@telefono", SqlDbType.VarChar);
                comandoContactoNuevo.Parameters["@mail"].Value = contacto.Mail;
                comandoContactoNuevo.Parameters["@direccion"].Value = contacto.Direccion;
                comandoContactoNuevo.Parameters["@telefono"].Value = contacto.Telefono;
                //Ejecuta el comando SQL
                con.Open();
                comandoContactoNuevo.ExecuteNonQuery();
                con.Close();
                //Obtiene el ID del contacto que se acaba de crear (no se pueden crear dos contactos con el mismo email para que esto funcione)(se puede agregar telefono y direccion para hacerlo mas seguro)
                SqlCommand comandoIDContactoNuevo = new SqlCommand("select ID from Contacto where Mail = @mail", con);
                comandoIDContactoNuevo.Parameters.Add("@mail", SqlDbType.VarChar);
                comandoIDContactoNuevo.Parameters["@mail"].Value = contacto.Mail;

                //Ejecuta un reader del comando anterior y pone el valor del ID obtenido en una variable
                con.Open();
                SqlDataReader idContactoNuevo = comandoIDContactoNuevo.ExecuteReader();
                while (idContactoNuevo.Read())
                {
                    id = Convert.ToInt32(idContactoNuevo[0]);
                }
                idContactoNuevo.Close();
                con.Close();
                //Ahora que tiene un ID de un contacto, crea una Empresa
                SqlCommand comandoEmpresaNueva = new SqlCommand("insert into Empresa(ID_Contacto, Nombre, CUIT) values (@ID_Contacto,@Nombre,@CUIT)", con);
                comandoEmpresaNueva.Parameters.Add("@ID_Contacto", SqlDbType.Int);
                comandoEmpresaNueva.Parameters.Add("@Nombre", SqlDbType.VarChar);
                comandoEmpresaNueva.Parameters.Add("@CUIT", SqlDbType.VarChar);
                comandoEmpresaNueva.Parameters["@ID_Contacto"].Value = id;
                comandoEmpresaNueva.Parameters["@Nombre"].Value = empresa.Nombre;
                comandoEmpresaNueva.Parameters["@CUIT"].Value = empresa.CUIT;

                con.Open();
                comandoEmpresaNueva.ExecuteNonQuery();
                con.Close();

                //Obtiene el ID de la Empresa recien creada
                SqlCommand comandoIDEmpresaNueva = new SqlCommand("select ID from Empresa where CUIT = @CUIT", con);
                comandoIDEmpresaNueva.Parameters.Add("@CUIT", SqlDbType.VarChar);
                comandoIDEmpresaNueva.Parameters["@CUIT"].Value = empresa.CUIT;
                //Ejecuta el reader del comando anterior y asigna el valor del ID encontrado a la variable usada anteriormente
                con.Open();
                SqlDataReader idEmpresaNueva = comandoIDEmpresaNueva.ExecuteReader();
                while (idEmpresaNueva.Read())
                {
                    id = Convert.ToInt32(idEmpresaNueva[0]);
                }
                idEmpresaNueva.Close();
                con.Close();
                //Crea un producto utilizando el ID de Empresa recien obtenido
                SqlCommand comandoProducto = new SqlCommand("insert into Producto(ID_Empresa, Stock, Precio_Costo, Margen, Precio_Bruto, Precio_Neto, Nombre, Detalle, Tipo_Componente, Marca) values (@ID_Empresa, @Stock, @Precio_Costo, @Margen, @Precio_Bruto, @Precio_Neto, @Nombre, @Detalle, @Tipo_Componente, @Marca)", con);
                comandoProducto.Parameters.Add("@ID_Empresa", SqlDbType.Int);
                comandoProducto.Parameters.Add("@Stock", SqlDbType.Int);
                comandoProducto.Parameters.Add("@Precio_Costo", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Margen", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Precio_Bruto", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Precio_Neto", SqlDbType.Float);
                comandoProducto.Parameters.Add("@Nombre", SqlDbType.VarChar);
                comandoProducto.Parameters.Add("@Detalle", SqlDbType.VarChar);
                comandoProducto.Parameters.Add("@Tipo_Componente", SqlDbType.VarChar);
                comandoProducto.Parameters.Add("@Marca", SqlDbType.VarChar);
                comandoProducto.Parameters["@ID_Empresa"].Value = id;
                comandoProducto.Parameters["@Stock"].Value = producto.Stock;
                comandoProducto.Parameters["@Precio_Costo"].Value = producto.PrecioCosto;
                comandoProducto.Parameters["@Margen"].Value = producto.Margen;
                comandoProducto.Parameters["@Precio_Bruto"].Value = producto.PrecioBruto;
                comandoProducto.Parameters["@Precio_Neto"].Value = producto.PrecioNeto;
                comandoProducto.Parameters["@Nombre"].Value = producto.Nombre;
                comandoProducto.Parameters["@Detalle"].Value = producto.Detalle;
                comandoProducto.Parameters["@Tipo_Componente"].Value = producto.TipoComponente;
                comandoProducto.Parameters["@Marca"].Value = producto.Marca;

                con.Open();
                int i = comandoProducto.ExecuteNonQuery();
                con.Close();

                return i;
            }

            
        }
        public List<Producto> NombreProducto()
        {
            Conectar();
            var listaNombreProductos = new List<Producto>();
            var traerNombre = new SqlCommand("select Nombre from Producto order by ID ASC", con);
            con.Open();
            SqlDataReader nombreProducto = traerNombre.ExecuteReader();
            while (nombreProducto.Read())
            {
                var i = 0;
                var prod = new Producto();
                prod.Nombre = nombreProducto[i].ToString();
                listaNombreProductos.Add(prod);
                i++;
            }
            nombreProducto.Close();
            con.Close();

            return listaNombreProductos;
        }
        public List<Producto> PrecioProducto()
        {
            Conectar();
            var listaPrecioProductos = new List<Producto>();
            var traerPrecio = new SqlCommand("select Precio_Neto from Producto order by ID ASC", con);
            con.Open();
            SqlDataReader precioProducto = traerPrecio.ExecuteReader();
            while (precioProducto.Read())
            {
                var i = 0;
                var prod = new Producto();
                prod.PrecioNetoGet = Convert.ToDouble(precioProducto[i]);
                listaPrecioProductos.Add(prod);
                i++;
            }
            precioProducto.Close();
            con.Close();

            return listaPrecioProductos;
        }
        public List<Empresa> NombreEmpresa()
        {
            Conectar();
            var listaNombreEmpresa = new List<Empresa>();
            var traerNombre = new SqlCommand("select Nombre from Empresa order by Nombre ASC", con);
            con.Open();
            SqlDataReader nombreEmpresa = traerNombre.ExecuteReader();
            while (nombreEmpresa.Read())
            {
                var i = 0;
                var emp = new Empresa();
                emp.Nombre = nombreEmpresa[i].ToString();
                listaNombreEmpresa.Add(emp);
                i++;
            }
            nombreEmpresa.Close();
            con.Close();
            return listaNombreEmpresa;
        }
    }
}