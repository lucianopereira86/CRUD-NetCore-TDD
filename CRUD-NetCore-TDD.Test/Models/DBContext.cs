using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_NetCore3._1_TDD.Test.Models
{
    public class DBContext
    {
        public DBContext()
        {
            User = new List<User>();
        }
        public List<User> User { get; set; }
    }
}
