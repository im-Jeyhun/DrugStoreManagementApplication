using DrugStoreManagementApplication.Congretes;
using DrugStoreManagementApplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreManagementApplication.DAL
{
    public class ConnectionParameters : AbstractConnectionParam
    {
        public ConnectionParameters() : base(
            new ConnectionParams().ServerIp(Config.DatabaseServer)
            .UserId("")
            .DatabaseName(Config.DatabaseName)
            .MaxPoolSize(Config.MaxPoolSize)
            )
        {
        }
    }
}
