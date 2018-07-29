using MultiThread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoNetKnowledge
{

    class Program
    {
        static void Main(string[] args)
        {
            /*
             * try catch finally 例子
             */
            //TryCatchFinallReturnDemo();

            /*
             * 基于Net 2.0  多线程中各种模型，及锁的使用例子
             * 1 锁的一些使用注意
             * 2 Monitor.Wait and Monitor.Pulse
             * 3 AutoResetEvent & ManualResetEvent
             */
            //MultiThreadPart();

            /*
             * await async 基本示例 
             */
            //AwaitAsyncSimpleDemo();
            //AwaitAsyncSimpleDemo2()

            /*
             * await async task 深入理解示例
             */
            AwaitAsyncAdvancedDemo();

        }

        #region try catch finally return
        static void TryCatchFinallReturnDemo()
        {
            Console.WriteLine(FinalCatchDemo.FinallyRetrunWithValueType());
            var user = FinalCatchDemo.FinallyRetrunWithObjectType();
            Console.WriteLine(user.Name + ":" + user.BirthDay);

            Console.ReadKey();
        }
        #endregion

        #region MultiThreadFor Net 2.0
        static void MultiThreadPart()
        {

            #region DoubleLookInOneMehtod

            //for (int i = 0; i < 3; i++)
            //{
            //    Thread t = new Thread(MonitorDemo.DoubleLookInOneMehtod);
            //    t.Start();
            //}

            #endregion

            #region Classic Wrong Lock Smaples

            #region lock public object

            //MonitorDemo monitordemo = new MonitorDemo();
            //lock (monitordemo) //获准了f对象
            //{
            //    ThreadStart ts = new ThreadStart(monitordemo.LockThis);
            //    Thread t = new Thread(ts);
            //    t.Start(); //新线程执行Bar方法需要获得f的访问权限，但是已被当前线程锁定，新线程将阻塞
            //    t.Join(); //新线程将无法返回，死锁
            //}

            #endregion


            #region 字符串的驻留（String Interning）

            //MonitorDemo monitordemo = new MonitorDemo();
            //lock ("Const") //获准了f对象
            //{
            //    ThreadStart ts = new ThreadStart(monitordemo.LockString);
            //    Thread t = new Thread(ts);
            //    t.Start(); //新线程执行Bar方法需要获得f的访问权限，但是已被当前线程锁定，新线程将阻塞
            //    t.Join(); //新线程将无法返回，死锁
            //}

            #endregion

            #endregion

            #region Monitor.Wait and Monitor.Pulse
            //MonitorDemo monitordemo = new MonitorDemo();

            //new Thread(monitordemo.WaitPulseMethodA).Start();
            //new Thread(monitordemo.WaitPulseMethodB).Start();
            //new Thread(monitordemo.WaitPulseMethodC).Start();
            //Console.ReadLine();

            #endregion

            #region AutoResetEvent & ManualResetEvent

            //EventHandleDemo eventhandledemo = new EventHandleDemo();
            ////eventhandledemo.AutoResetEventMethod();

            ////eventhandledemo.AutoRestEventMethod2();
            //eventhandledemo.EventHandleMethod();


            #endregion
        }

        #endregion

        #region AwaitAsyncSimpleDemo
        static void AwaitAsyncSimpleDemo()
        {
            //awaitasyncsimpletest(); // 这个方法其实是多余的, 本来可以直接写下面的方法

            // await GetName() //注：这句话其实会报错，不支持async的方法，不能使用 await
            // 但是由于控制台的入口方法不支持async,所有我们在入口方法里面不能 用 await

            Console.WriteLine("Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        static async Task awaitasyncsimpletest()
        {
            Console.WriteLine("awaitasyncsimpletest Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId);

            // 方法打上async关键字，就可以用await调用同样打上async的方法
            // await 后面的方法将在另外一个线程中执行
            await getName();

            Console.WriteLine("Test Completed Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId);
        }

        static async Task getName()
        {
            // Delay 方法来自于.net 4.5
            await Task.Delay(1000);  // 返回值前面加 async 之后，方法里面就可以用await了
            Console.WriteLine("GetName Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("In antoher thread.....");
        }


        static void AwaitAsyncSimpleDemo2()
        {
            AsyncAwaitDemo asyncawaitdemo = new AsyncAwaitDemo();

            // 同步方式
            Console.WriteLine("同步方式测试开始！");
            asyncawaitdemo.SyncMethod(0);
            Console.WriteLine("同步方式结束！");
            Console.ReadKey();

            // 异步方式
            Console.WriteLine("\n异步方式测试开始！");
            asyncawaitdemo.AsyncMethod(0);
            Console.WriteLine("异步方式结束！");
            Console.ReadKey();
        }

        #endregion

        #region AwaitAsyncAdvancedDemo

        static void AwaitAsyncAdvancedDemo()
        {

            #region task and await
            Console.WriteLine("Thread {0},主线程", Thread.CurrentThread.ManagedThreadId);

            AsyncAwaitDemo asyncawaitdemo = new AsyncAwaitDemo();

            asyncawaitdemo.AwaitMethod();


            #region await &  task.GetAwaiter() difference

            //asyncawaitdemo.TaskMethod();           

            //asyncawaitdemo.TaskMethod2();


            #endregion

            #region await 本质

            //asyncawaitdemo.AwaitTaskMethod();

            #endregion

            Console.ReadKey();

            #endregion
        }

        #endregion

    }

}
