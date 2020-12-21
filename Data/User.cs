using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Data
{
    public class User
    {
        public long id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

    }
}
