using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread
{
    public class EventHandleDemo
    {

        // 初始化自动重置事件，并把状态设置为非终止状态  
        // 如果这里把初始状态设置为True时，  
        // 当调用WaitOne方法时就不会阻塞线程,看到的输出结果的时间就是一样的了  
        // 因为设置为True时，表示此时已经为终止状态了。         
        public static AutoResetEvent autoEvent = new AutoResetEvent(false);
        public void AutoResetEventMethod()
        {
            Console.WriteLine("(AutoResetEvent) Main Thread Start run at: " + DateTime.Now.ToLongTimeString());
            //Thread t = new Thread(AutoResetEventTestMethod);
            Thread t = new Thread(AutoResetEventTestMethodWithTimeOut);
            t.Start();

            // 阻塞主线程3秒后  
            // 调用 Set方法释放线程，使线程t可以运行  
            Thread.Sleep(3000);

            // Set 方法就是把事件状态设置为终止状态。  
            autoEvent.Set();
            Console.Read();
        }

        private void AutoResetEventTestMethod()
        {
            autoEvent.WaitOne();

            // 3秒后线程可以运行，所以此时显示的时间应该和主线程显示的时间相差3秒  
            Console.WriteLine("(AutoResetEvent) AutoResetEventTestMethod Restart run at: " + DateTime.Now.ToLongTimeString());
        }

        private void AutoResetEventTestMethodWithTimeOut()
        {
            if (autoEvent.WaitOne(2000))
            {
                Console.WriteLine("AutoResetEventTestMethodWithTimeOut Get Singal to Work");
                // 3秒后线程可以运行，所以此时显示的时间应该和主线程显示的时间相差一秒  
                Console.WriteLine("AutoResetEventTestMethodWithTimeOut Method Restart run at: " + DateTime.Now.ToLongTimeString());
            }
            else
            {
                Console.WriteLine("AutoResetEventTestMethodWithTimeOut Time Out to work");
                Console.WriteLine("AutoResetEventTestMethodWithTimeOut Method Restart run at: " + DateTime.Now.ToLongTimeString());
            }
        }



        public static AutoResetEvent autoEvent2 = new AutoResetEvent(true);
        //public static ManualResetEvent autoEvent2 = new ManualResetEvent(true); 
        public void AutoRestEventMethod2()
        {
            Console.WriteLine("(AutoRestEventMethod2) Main Thread Start run at: " + DateTime.Now.ToLongTimeString());
            Thread t = new Thread(AutoResetEventTwoWaitMethod);
            t.Start();
            Console.Read();
        }

        private void AutoResetEventTwoWaitMethod()
        {
            // 初始状态为终止状态，则第一次调用WaitOne方法不会堵塞线程  
            // 此时运行的时间间隔应该为0秒，但是因为是AutoResetEvent对象  
            // 调用WaitOne方法后立即把状态返回为非终止状态。  
            autoEvent2.WaitOne();
            Console.WriteLine("AutoResetEventTwoWaitMethod Method start at : " + DateTime.Now.ToLongTimeString());

            // 因为此时AutoRestEvent为非终止状态，所以调用WaitOne方法后将阻塞线程1秒，这里设置了超时时间  
            // 所以下面语句的和主线程中语句的时间间隔为1秒  
            // 当时 ManualResetEvent对象时，因为不会自动重置状态  
            // 所以调用完第一次WaitOne方法后状态仍然为非终止状态,所以再次调用不会阻塞线程，所以此时的时间间隔也为0  
            // 如果没有设置超时时间的话，下面这行语句将不会执行  
            autoEvent2.WaitOne(1000);
            Console.WriteLine("AutoResetEventTwoWaitMethod Method start at : " + DateTime.Now.ToLongTimeString());


        }


        /*
         * 开启多个进程方式使用
         * 
         */
        //使用非静态变量也一样
        public static EventWaitHandle eventautoEvent = new EventWaitHandle(true, EventResetMode.AutoReset, "My");
        public void EventHandleMethod()
        {
            Console.WriteLine("(EventHandleMethod)Main Thread Start run at: " + DateTime.Now.ToLongTimeString());
            Thread t = new Thread(TestEventHandleMethod);

            // 为了有时间启动另外一个线程  
            Thread.Sleep(2000);
            t.Start();
            Console.Read();
        }

        private void TestEventHandleMethod()
        {
            // 进程一：显示的时间间隔为2秒  
            // 进程二中显示的时间间隔为3秒  
            // 因为进程二中AutoResetEvent的初始状态为非终止的  
            // 因为在进程一中通过WaitOne方法的调用已经把AutoResetEvent的初始状态返回为非终止状态了  
            eventautoEvent.WaitOne(1000);
            Console.WriteLine("TestEventHandleMethod start at : " + DateTime.Now.ToLongTimeString());
        }

    }
}
