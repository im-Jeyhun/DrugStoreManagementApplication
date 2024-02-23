using DrugStoreManagementApplication.Model;
using DrugStoreManagementApplication.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrugStoreManagementApplication
{
    public partial class LoginArea : Form
    {
        public LoginArea()
        {
            InitializeComponent();
        }

        private void LoginArea_Load(object sender, EventArgs e)
        {
            var user = new User
            {
                FName = "Ceyhun",
                FatherName = "Afqan"
            };
            var data = JsonConvert.SerializeObject(user);

            //var rep = new UserRpository();
            //rep.CreateUser(user);
           
        }
    }
}
