using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetKnowledge
{
    public class FinalCatchDemo
    {
        /*
         * 在一段try catch finally 语句块中，finally是最后执行的
         * 下面两个例子 有return语句的时候，也是先执行return再执行finally语句
         * 需要注意的是：
         * return 带有返回值时，返回变量在finally语句块中被 修改时，最后函数运行结束返回的结果是什么
         * 值类型时，return之后，finally修改，不会影响其值（return之后，其实变量已经返回，值类型的修改，并不会影响返回的那个值）
         * 引用类型时，return之后，finally修改，会影响其值 （return之后，返回的引用类型变量 和 函数内的变量指向的同一块区域）
         * 
         */

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
