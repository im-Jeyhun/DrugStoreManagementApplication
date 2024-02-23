using DrugStoreManagementApplication.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreManagementApplication.Model
{
    public class User : BaseEntity<int>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string FatherName { get; set; }
        public string UName { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
    }
}
