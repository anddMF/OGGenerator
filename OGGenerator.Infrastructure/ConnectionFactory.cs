using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OGGenerator.Infrastructure
{
    public class ConnectionFactory
    {
        private SqlConnection Connection;

        private SqlConnection GetConnection()
        {
            var connString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;

            if (Connection == null)
                Connection = new SqlConnection(connString);

            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            return Connection;
        }

        public SqlCommand GetCommand()
        {
            return this.GetConnection().CreateCommand();
        }

        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
            Connection.Dispose();
        }

        public SqlDataReader GetReader(string cmdText,
            CommandType cmdType = CommandType.Text,
            Dictionary<string, object> parametros = null
            )
        {
            using (var cmd = this.GetCommand())
            {
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;

                if (parametros != null)
                    foreach (var pr in parametros)
                        cmd.Parameters.AddWithValue(pr.Key, pr.Value);

                return cmd.ExecuteReader(); //SELECT , PROC QUE RETORNEM TABELA (n LINHAS E n COLUNAS)
            }
        }

        public bool ExecuteNonQuery(string cmdText,
            CommandType cmdType = CommandType.Text,
            Dictionary<string, object> parametros = null)
        {
            try
            {
                using (var cmd = this.GetCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;

                    if (parametros != null)
                        foreach (var pr in parametros)
                            cmd.Parameters.AddWithValue(pr.Key, pr.Value);

                    cmd.ExecuteNonQuery(); //INSERT, DELETE, UPDATE, PROC SEM RETORNO
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public object ExecuteScalar(string cmdText,
            CommandType cmdType = CommandType.Text,
            Dictionary<string, object> parametros = null)
        {
            try
            {
                using (var cmd = this.GetCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;

                    if (parametros != null)
                        foreach (var pr in parametros)
                            cmd.Parameters.AddWithValue(pr.Key, pr.Value);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
