using DrugStoreManagementApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DrugStoreManagementApplication.DAL;
using DrugStoreManagementApplication.Congretes;
using Newtonsoft.Json;
using System.Data;
namespace DrugStoreManagementApplication.Repository
{
    public class UserRpository : ConnectionParameters
    {
        private static UserRpository _userRpository;
        public UserRpository()
        {
        }

        private static UserRpository GetUserRpository() => _userRpository ?? (_userRpository = new UserRpository());

        public void CreateUser(User user)
        {
            using (var connection = new DbConnect(ConParameters))
            {
                var data = JsonConvert.SerializeObject(user);
                connection.ExecuteStoredProcedure("CreateUser", new SqlParameter[]
                {
                    DbConnect.SetParameter("@UserJson", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, data)
                });
            }
        }
    }
}
