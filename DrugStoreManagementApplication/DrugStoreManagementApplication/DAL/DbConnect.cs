using DrugStoreManagementApplication.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreManagementApplication.DAL
{
    public class DbConnect : IDisposable
    {
        private readonly string _connectionString;

        private SqlConnection _conn;

        private bool _disposed;

        public SqlConnection GetConnection()
        {
            return _conn;
        }

        public DbConnect(ConnectionParams connectionParams)
        {
            _connectionString = connectionParams.BuildSqlConnectionString();
            Connect();
        }

        public DbConnect(string connectionString)
        {
            _connectionString = connectionString;
            Connect();
        }

        private void Connect()
        {
            _conn = new SqlConnection(_connectionString);
            _conn.Open();
        }

        public void OpenConnection()
        {
            if (_conn == null)
            {
                _conn = new SqlConnection(_connectionString);
            }

            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
        }

        public void CloseConnection()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        public bool IsConAlive()
        {
            try
            {
                return _conn == null || _conn.State == ConnectionState.Closed || _conn.State == ConnectionState.Broken;
            }
            catch
            {
                return false;
            }
        }

        private SqlCommand GetCommand(string sqlQuery)
        {
            return GetCommand(sqlQuery, null);
        }

        private SqlCommand GetCommand(string sqlQuery, SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, _conn)
            {
                CommandTimeout = 3600
            };
            if (parameters == null)
            {
                return sqlCommand;
            }

            foreach (SqlParameter value in parameters)
            {
                sqlCommand.Parameters.Add(value);
            }

            return sqlCommand;
        }

        public SqlDataReader ExecuteSql(string sqlQuery)
        {
            return ExecuteSql(sqlQuery, null);
        }

        public SqlDataReader ExecuteSql(string sqlQuery, SqlParameter[] parameters)
        {
            SqlCommand command = GetCommand(sqlQuery, parameters);
            return command.ExecuteReader(CommandBehavior.KeyInfo);
        }

        public static SqlParameter SetParameter(string parameterName, SqlDbType parameterType, int size, ParameterDirection direction, object value)
        {
            return new SqlParameter(parameterName, parameterType, size)
            {
                Direction = direction,
                Value = value
            };
        }

        public void ExecuteStoredProcedure(string storeProcedureName)
        {
            ExecuteStoredProcedure(storeProcedureName, null);
        }

        public void ExecuteStoredProcedure(string storeProcedureName, SqlParameter[] parameters)
        {
            SqlCommand command = GetCommand(storeProcedureName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteScalar();
        }

        public object ExecStoredProcWithReturnIntValue(string storeProcedureName)
        {
            return ExecuteStoredProcedureWithReturnValue(storeProcedureName, null);
        }

        public int ExecStoredProcWithReturnIntValue(string storeProcedureName, SqlParameter[] parameters, Action<SqlInfoMessageEventArgs> infoMsgCallback)
        {
            _conn.FireInfoMessageEventOnUserErrors = true;
            _conn.InfoMessage += InfoDel;
            int result = ExecStoredProcWithReturnIntValue(storeProcedureName, parameters);
            _conn.InfoMessage -= InfoDel;
            _conn.FireInfoMessageEventOnUserErrors = false;
            return result;
            void InfoDel(object sender, SqlInfoMessageEventArgs e)
            {
                infoMsgCallback(e);
            }
        }

        public int ExecStoredProcWithReturnIntValue(string storeProcedureName, SqlParameter[] parameters)
        {
            SqlCommand command = GetCommand(storeProcedureName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlParameter = command.Parameters.Add("@RET_VAL", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            return ((int?)sqlParameter.Value).GetValueOrDefault();
        }

        public object ExecuteStoredProcedureWithReturnValue(string storeProcedureName)
        {
            return ExecuteStoredProcedureWithReturnValue(storeProcedureName, null);
        }

        public object ExecuteStoredProcedureWithReturnValue(string storeProcedureName, SqlParameter[] parameters)
        {
            SqlCommand command = GetCommand(storeProcedureName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            return command.ExecuteScalar();
        }

        public DataTable GetDataTable(string sqlQuery)
        {
            return GetDataTable(sqlQuery, null);
        }

        public DataTable GetDataTable(string sqlQuery, SqlParameter[] parameters)
        {
            SqlCommand command = GetCommand(sqlQuery, parameters);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _conn.Dispose();
                }

                _disposed = true;
            }
        }

        ~DbConnect()
        {
            Dispose(disposing: false);
        }
    }

}
