using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreManagementApplication.Utils
{
    public class ConnectionParams
    {
        private string _serverIp;

        private string _databaseName;

        private string _userId;

        private string _password;

        private bool _trustedConnection;

        private int _connectionTimeout = 30;

        private int _maxPoolSize;

        public ConnectionParams ServerIp(string serverIp)
        {
            _serverIp = serverIp;
            return this;
        }

        public ConnectionParams DatabaseName(string databaseName)
        {
            _databaseName = databaseName;
            return this;
        }

        public ConnectionParams UserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public ConnectionParams Password(string password)
        {
            _password = password;
            return this;
        }

        public ConnectionParams TrustedConnection(bool trustedConnection)
        {
            _trustedConnection = trustedConnection;
            return this;
        }

        public ConnectionParams ConnectionTimeout(int connectionTimeout)
        {
            _connectionTimeout = connectionTimeout;
            return this;
        }

        public ConnectionParams MaxPoolSize(int maxPoolSize)
        {
            _maxPoolSize = maxPoolSize;
            return this;
        }

        public string BuildSqlConnectionString()
        {
            if (_serverIp == null)
            {
                throw new ArgumentException("ServerIP cannot be null");
            }

            if (_databaseName == null)
            {
                throw new ArgumentException("DatabaseName cannot be null");
            }

            if (_userId == null)
            {
                throw new ArgumentException("Database User cannot be null");
            }

            if (_maxPoolSize <= 0)
            {
                throw new ArgumentException("MaxPoolSize must be greater than zero");
            }

            StringBuilder stringBuilder = new StringBuilder("Server=").Append(_serverIp).Append(";").Append("Database=")
                .Append(_databaseName)
                .Append(";")
                .Append("User ID=")
                .Append(_userId)
                .Append(";")
                .Append("Password=")
                .Append(_password)
                .Append(";")
                .Append("Trusted_Connection=")
                .Append(_trustedConnection)
                .Append(";")
                .Append("Connection Timeout=")
                .Append(_connectionTimeout)
                .Append(";")
                .Append("Pooling=")
                .Append(value: true)
                .Append(";")
                .Append("Max Pool Size=")
                .Append(_maxPoolSize)
                .Append(";");
            return stringBuilder.ToString();
        }
    }

}
