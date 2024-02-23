using DrugStoreManagementApplication.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreManagementApplication.Model
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
