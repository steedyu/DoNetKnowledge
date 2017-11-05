using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetKnowledge
{
    public class FinalCatchDemo
    {
        public static int FinallyRetrunWithValueType()
        {
            int i = 0;
            try
            {
                return i;
            }
            finally
            {
                i = 2;
            }
        }

        public static User FinallyRetrunWithObjectType()
        {
            User user = new User()
            {
                Name = "Mike",
                BirthDay = new DateTime(2010, 1, 1)
            };
            try
            {
                return user;
            }
            finally
            {
                user.Name = "Rose";
                user.BirthDay = new DateTime(2010, 2, 2);
                Console.WriteLine("UserName Has changed");
                user = null;
            }
        }
    }

    public class User
    {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
