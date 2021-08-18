using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CoinJar.Data
{
    public static class Utilities
    {
        #region Enum

        [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
        private sealed class EnumDescriptionAttribute : Attribute
        {
            public string Description { get; }

            public EnumDescriptionAttribute(string description) : base()
            {
                Description = description;
            }
        }

        public static class EnumHelper
        {
            public static string GetDescription(Enum value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                string description = value.ToString();
                FieldInfo fieldInfo = value.GetType().GetField(description);
                EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    description = attributes[0].Description;
                }
                return description;
            }
        }

        #endregion

        public static class DatabaseHelper
        {
            public static string ConnectionString { get; set; }

            public static async Task<SqlDataReader> ExecuteSqlDataReader(SqlConnection connection, string storedProcedureName, ArrayList parameters)
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                if (parameters == null)
                    parameters = new();

                SqlCommand _comm = new(storedProcedureName, connection);
                foreach (SqlParameter param in parameters)
                {
                    _comm.Parameters.Add(param);
                }
                _comm.CommandType = CommandType.StoredProcedure;
                return await _comm.ExecuteReaderAsync();
            }

            public static async Task<int> ExecuteNonQuery(SqlConnection connection, string storedProcedureName, ArrayList parameters)
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                if (parameters == null)
                    parameters = new();

                SqlCommand _comm = new(storedProcedureName, connection);
                foreach (SqlParameter param in parameters)
                {
                    _comm.Parameters.Add(param);
                }
                _comm.CommandType = CommandType.StoredProcedure;
                return await _comm.ExecuteNonQueryAsync();
            }

            public static async Task<DataTable> ExecuteQuery(SqlConnection connection, string storedProcedureName, ArrayList parameters)
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                if (parameters == null)
                    parameters = new();

                SqlDataAdapter _adapter = new() { SelectCommand = new SqlCommand(storedProcedureName, connection) };
                foreach (SqlParameter param in parameters)
                {
                    _adapter.SelectCommand.Parameters.Add(param);
                }

                _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet _ds = new DataSet();
                await Task.Run(() => _adapter.Fill(_ds, "tableData"));
                return _ds.Tables["tableData"];
            }

            public static async Task<object> ExecuteScaler(SqlConnection connection, string storedProcedureName, ArrayList parameters)
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                if (parameters == null)
                    parameters = new();

                SqlCommand _command = new(storedProcedureName, connection) { CommandType = CommandType.StoredProcedure };
                foreach (SqlParameter param in parameters)
                {
                    _command.Parameters.Add(param);
                }

                try
                {
                    return await _command.ExecuteScalarAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            static public SqlParameter CreateIntParameter(string name, int value)
            {
                return new SqlParameter(name, SqlDbType.Int) { Value = value };
            }
            static public SqlParameter CreateDecimalParameter(string name, decimal value)
            {
                return new SqlParameter(name, SqlDbType.Decimal) { Value = value };
            }
            static public SqlParameter CreateStringParameter(string name, string value)
            {
                return new SqlParameter(name, SqlDbType.NVarChar) { Value = value };
            }
            static public SqlParameter CreateDateTimeParameter(string name, DateTime value)
            {
                return new SqlParameter(name, SqlDbType.DateTime) { Value = value };
            }
            static public SqlParameter CreateUniqueIdentifierParameter(string name, Guid value)
            {
                return new SqlParameter(name, SqlDbType.UniqueIdentifier) { Value = value };
            }
        }

        public static class DateHelper
        {
            public static DateTime DefaultDate
            {
                get
                {
                    return Convert.ToDateTime("9995-01-01");
                }
            }
        }
    }
}
