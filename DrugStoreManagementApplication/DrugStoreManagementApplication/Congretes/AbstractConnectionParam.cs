using DrugStoreManagementApplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreManagementApplication.Congretes
{
    public class AbstractConnectionParam
    {
        protected ConnectionParams ConParameters { get; }
        public AbstractConnectionParam(ConnectionParams conParameters)
        {
            ConParameters = conParameters;
        }

    }
}
