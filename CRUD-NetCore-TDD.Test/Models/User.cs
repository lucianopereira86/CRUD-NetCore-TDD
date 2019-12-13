using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_NetCore3._1_TDD.Test.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public User(int Id, string Name, int Age)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
