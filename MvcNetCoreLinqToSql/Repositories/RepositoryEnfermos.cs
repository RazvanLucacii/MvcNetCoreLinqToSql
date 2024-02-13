using MvcNetCoreLinqToSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcNetCoreLinqToSql.Repositories
{
    public class RepositoryEnfermos
    {
        SqlConnection connection;
        SqlCommand command;

        private DataTable tablaEnfermos;

        public RepositoryEnfermos() 
        {
            string sql = "select * from ENFERMO";
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            SqlDataAdapter adEnf = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();
            adEnf.Fill(tablaEnfermos);

            this.command = new SqlCommand();
            this.connection = new SqlConnection(connectionString);
            this.command.Connection = this.connection;
        }

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           select datos;
            List<Enfermo> enfermos = new List<Enfermo>();
            foreach (var row in consulta)
            {
                Enfermo enfermo = new Enfermo();
                enfermo.Inscripcion = row.Field<string>("INSCRIPCION");
                enfermo.Apellido = row.Field<string>("APELLIDO");
                enfermo.Direccion = row.Field<string>("DIRECCION");
                enfermo.Fecha_Nac = row.Field<DateTime>("FECHA_NAC");
                enfermo.S = row.Field<string>("S");
                enfermo.NSS = row.Field<string>("NSS");
                enfermos.Add(enfermo);
            }
            return enfermos;
        }

        public Enfermo FindEnfermo(string inscripcion)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<string>("INSCRIPCION") == inscripcion
                           select datos;
            var row = consulta.First();

            Enfermo enfermo = new Enfermo();
            enfermo.Inscripcion = row.Field<string>("INSCRIPCION");
            enfermo.Apellido = row.Field<string>("APELLIDO");
            enfermo.Direccion = row.Field<string>("DIRECCION");
            enfermo.Fecha_Nac = row.Field<DateTime>("FECHA_NAC");
            enfermo.S = row.Field<string>("S");
            enfermo.NSS = row.Field<string>("NSS");

            return enfermo;
        }

        public async Task DeleteAsync(string inscripcion)
        {
            string sql = "delete from ENFERMO where INSCRIPCION=@inscripcion";

            this.command.Parameters.AddWithValue("@inscripcion", inscripcion);
            this.command.CommandText = sql;
            this.command.CommandType = CommandType.Text;

            await this.connection.OpenAsync();
            await this.command.ExecuteNonQueryAsync();
            await this.connection.CloseAsync();

            this.command.Parameters.Clear();
        }
    }
}
