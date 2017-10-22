using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread
{

    public class MonitorDemo
    {
        private static readonly object obj = new object();

        /// <summary>
        /// .Net一个方法中，连续锁定同一个对象两次，在第一次锁定的代码范围内再次锁定，第二次锁并不会进行加锁
        /// </summary>

        public static void DoubleLookInOneMehtod()
        {
            Thread.Sleep(500);
            lock (obj)
            {
                Console.WriteLine(string.Format("Thread No.{0} {1}", Thread.CurrentThread.ManagedThreadId, "DoubleLookInOneMehtod 1st"));

                lock (obj)
                {
                    Console.WriteLine(string.Format("Thread No.{0} {1}", Thread.CurrentThread.ManagedThreadId, "DoubleLookInOneMehtod 2nd"));

                    Thread.Sleep(3000);
                }
            }
        }

        /// <summary>
        /// lock public object
        /// </summary>
        public void LockThis()
        {
            lock (this)
            {
                Console.WriteLine("MonitorDemo LockThis");
            }
        }

        /// <summary>
        /// 字符串的驻留（String Interning）
        /// </summary>
        public void LockString()
        {
            lock ("Const")
            {
                Console.WriteLine("LockString");
            }
        }


        #region Wait and Pulse

        public void WaitPulseMethodA()
        {
            lock (obj)
            //进入就绪队列 
            {
                Thread.Sleep(1000);
                Monitor.Pulse(obj);
                Monitor.Wait(obj); //自我流放到等待队列 
            } Console.WriteLine("A exit...");
        }
        public void WaitPulseMethodB()
        {
            Thread.Sleep(500);
            lock (obj) //进入就绪队列 
            {
                Monitor.Pulse(obj);
                Thread.Sleep(100);
            } Console.WriteLine("B exit...");
        }
        public void WaitPulseMethodC()
        {
            Thread.Sleep(800);
            lock (obj) //进入就绪队列 
            { }
            Console.WriteLine("C exit...");
        }

        #endregion


    }



}
