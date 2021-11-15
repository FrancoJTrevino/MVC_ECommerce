using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MVC_ECommerce.Models
{
    public class AdministracionCliente
    {
        private SqlConnection con;

        private void Conectar()
        {
            //Crea una conexion a la base de datos
            string constr = "Data Source = DESKTOP-PH3T3KJ\\SQLEXPRESS; Initial Catalog = ECommerce; Integrated Security = True";
            con = new SqlConnection(constr);
        }

        public int AltaCliente(Contacto contacto, Persona persona, Cliente cliente)
        {
            Conectar();
            //Crea comando SQL para insertar un contacto primero
            SqlCommand comandoContacto = new SqlCommand("insert into Contacto(Mail,Direccion,Telefono) values (@mail,@direccion,@telefono)", con);
            comandoContacto.Parameters.Add("@mail", SqlDbType.VarChar);
            comandoContacto.Parameters.Add("@direccion", SqlDbType.VarChar);
            comandoContacto.Parameters.Add("@telefono", SqlDbType.VarChar);
            comandoContacto.Parameters["@mail"].Value = contacto.Mail;
            comandoContacto.Parameters["@direccion"].Value = contacto.Direccion;
            comandoContacto.Parameters["@telefono"].Value = contacto.Telefono;
            //Ejecuta el comando SQL
            con.Open();
            comandoContacto.ExecuteNonQuery();
            con.Close();
            //Obtiene el ID del contacto que se acaba de crear (no se pueden crear dos contactos con el mismo email para que esto funcione)(se puede agregar telefono y direccion para hacerlo mas seguro)
            SqlCommand comandoIDContacto = new SqlCommand("select ID from Contacto where Mail = @mail",con);
            comandoIDContacto.Parameters.Add("@mail", SqlDbType.VarChar);
            comandoIDContacto.Parameters["@mail"].Value = contacto.Mail;

            int id = 0;
            //Ejecuta un reader del comando anterior y pone el valor del ID obtenido en una variable
            con.Open();
            SqlDataReader idContacto = comandoIDContacto.ExecuteReader();
            while (idContacto.Read())
            {
                id = Convert.ToInt32(idContacto[0]);
            }
            idContacto.Close();
            con.Close();
            //Ahora que tiene un ID de un contacto, crea una persona (la cual necesita obligatoriamente un ID_Contacto para crearse)
            SqlCommand comandoPersona = new SqlCommand("insert into Persona(ID_Contacto, Nombre, Apellido, CUIL, DNI, Fecha_Nac) values (@ID_Contacto, @Nombre, @Apellido, @CUIL, @DNI, @Fecha_Nac)", con);
            comandoPersona.Parameters.Add("@ID_Contacto", SqlDbType.Int);
            comandoPersona.Parameters.Add("@Nombre", SqlDbType.VarChar);
            comandoPersona.Parameters.Add("@Apellido", SqlDbType.VarChar);
            comandoPersona.Parameters.Add("@CUIL", SqlDbType.Char);
            comandoPersona.Parameters.Add("@DNI", SqlDbType.Char);
            comandoPersona.Parameters.Add("@Fecha_Nac", SqlDbType.DateTime);
            comandoPersona.Parameters["@ID_Contacto"].Value = id;
            comandoPersona.Parameters["@Nombre"].Value = persona.Nombre;
            comandoPersona.Parameters["@Apellido"].Value = persona.Apellido;
            comandoPersona.Parameters["@CUIL"].Value = persona.CUIL;
            comandoPersona.Parameters["@DNI"].Value = persona.DNI;
            comandoPersona.Parameters["@Fecha_Nac"].Value = persona.FechaNac;

            con.Open();
            comandoPersona.ExecuteNonQuery();
            con.Close();
            //Obtiene el ID de la persona creada anteriormente (no se pueden crear dos usuarios con el mismo CUIL para que esto funcione, aunque nadie deberia tener el mismo CUIL en primer lugar...)
            SqlCommand comandoIDPersona = new SqlCommand("select ID from Persona where CUIL = @CUIL", con);
            comandoIDPersona.Parameters.Add("@CUIL", SqlDbType.Char);
            comandoIDPersona.Parameters["@CUIL"].Value = persona.CUIL;
            //Ejecuta el reader del comando anterior y asigna el valor del ID encontrado a la variable usada anteriormente
            con.Open();
            SqlDataReader idPersona = comandoIDPersona.ExecuteReader();
            while (idPersona.Read())
            {
                id = Convert.ToInt32(idPersona[0]);
            }
            idPersona.Close();
            con.Close();
            //Crea un cliente utilizando como ID_Persona el ID obtenido anteriormente
            SqlCommand comandoCliente = new SqlCommand("insert into Cliente(ID_Persona, FechaReg, Nombre_Usuario, Contra) values (@ID_Persona, @FechaReg, @Nombre_Usuario, @Contra)", con);
            comandoCliente.Parameters.Add("@ID_Persona", SqlDbType.Int);
            comandoCliente.Parameters.Add("@FechaReg", SqlDbType.DateTime);
            comandoCliente.Parameters.Add("@Nombre_Usuario", SqlDbType.VarChar);
            comandoCliente.Parameters.Add("@Contra", SqlDbType.VarChar);
            comandoCliente.Parameters["@ID_Persona"].Value = id;
            comandoCliente.Parameters["@FechaReg"].Value = cliente.FechaReg;
            comandoCliente.Parameters["@Nombre_Usuario"].Value = cliente.Nombre_Usuario;
            comandoCliente.Parameters["@Contra"].Value = cliente.Password;

            con.Open();
            int i = comandoCliente.ExecuteNonQuery();
            con.Close();

            return i;
        }
        public bool IniciarSesion(Cliente cliente)
        {
            Conectar();
            bool a;
            var comandoCliente = new SqlCommand("select Nombre_Usuario, Contra from Cliente where Nombre_Usuario = @Nombre_Usuario and Contra = @Contra", con);
            comandoCliente.Parameters.Add("@Nombre_Usuario", SqlDbType.VarChar);
            comandoCliente.Parameters.Add("@Contra", SqlDbType.VarChar);
            comandoCliente.Parameters["@Nombre_Usuario"].Value = cliente.Nombre_Usuario;
            comandoCliente.Parameters["@Contra"].Value = cliente.Password;

            con.Open();
            var existeCliente = comandoCliente.ExecuteReader();
            if(existeCliente.Read() == true)
            {
                a = true;
            }
            else
            {
                a = false;
            }
            existeCliente.Close();
            con.Close();

            return a;
        }
    }
}