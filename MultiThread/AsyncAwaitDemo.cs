using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread
{
    public class AsyncAwaitDemo
    {
        #region sync and async await

        // 同步操作
        public void SyncMethod(int input)
        {
            Console.WriteLine("进入同步操作！");
            var result = SyancWork(input);
            Console.WriteLine("最终结果{0}", result);
            Console.WriteLine("退出同步操作！");
        }

        // 模拟耗时操作（同步方法）
        public int SyancWork(int val)
        {
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine("耗时操作{0}", i);
                Thread.Sleep(100);
                val++;
            }
            return val;
        }


        // 异步操作
        public async void AsyncMethod(int input)
        {
            Console.WriteLine("进入异步操作！");
            var result = await AsyncWork(input);
            Console.WriteLine("最终结果{0}", result);
            Console.WriteLine("退出异步操作！");
        }

        // 模拟耗时操作（异步方法）
        public async Task<int> AsyncWork(int val)
        {
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine("耗时操作{0}", i);
                await Task.Delay(100);
                val++;
            }
            return val;
        }

        #endregion

        #region task and await

        /// <summary>
        /// await并不是针对于async的方法，而是针对async方法所返回给我们的Task，这也是为什么所有的async方法都必须返回给我们Task。
        /// 所以我们同样可以在Task前面也加上await关键字，这样做实际上是告诉编译器我需要等这个Task的返回值或者等这个Task执行完毕之后才能继续往下走。
        /// </summary>
        public async void AwaitMethod()
        {
            Task<string> task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "Hello World";
            });
            string str = await task;  //5 秒之后才会执行这里
            Console.WriteLine(str);
        }

        /// <summary>
        /// 不用await关键字，如何确认Task执行完毕了？
        /// </summary>
        public void TaskMethod()
        {
            var task = Task.Run(() =>
            {
                Console.WriteLine("Thread {0},另外一个线程在获取名称", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
                return "Jesse";
            });

            /*
             * GetAwaiter方法会返回一个awaitable的对象（继承了INotifyCompletion.OnCompleted方法）我们只是传递了一个委托进去，等task完成了就会执行这个委托，但是并不会影响主线程，下面的代码会立即执行。这也是为什么我们结果里面第一句话会是 “主线程执行完毕”！
             */
            task.GetAwaiter().OnCompleted(() =>
            {
                // 2 秒之后才会执行这里
                var name = task.Result;
                Console.WriteLine("Thread {0},My name is: {1}", Thread.CurrentThread.ManagedThreadId, name);
            });

            Console.WriteLine("Thread {0},主线程执行完毕", Thread.CurrentThread.ManagedThreadId);
        }

        /* 加上await关键字之后，后面的代码会被挂起等待，直到task执行完毕有返回值的时候才会继续向下执行，这一段时间主线程会处于挂起状态。
         * GetAwaiter方法会返回一个awaitable的对象（继承了INotifyCompletion.OnCompleted方法）我们只是传递了一个委托进去，等task完成了就会执行这个委托，但是并不会影响主线程，下面的代码会立即执行。
         * 这也是为什么我们结果里面第一句话会是 “主线程执行完毕”！
         */

        /// <summary>
        /// Task如何让运行线程挂起等待？
        /// </summary>

        public void TaskMethod2()
        {
            var task = Task.Run(() =>
            {
                Console.WriteLine("Thread {0},另外一个线程在获取名称", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
                return "Jesse";
            });

            var name = task.GetAwaiter().GetResult();
            Console.WriteLine("Thread {0},My name is:{0}", Thread.CurrentThread.ManagedThreadId, name);

            Console.WriteLine("Thread {0},主线程执行完毕", Thread.CurrentThread.ManagedThreadId);
        }

        /*Task.GetAwait()方法会给我们返回一个awaitable的对象，通过调用这个对象的GetResult方法就会挂起主线程，当然也不是所有的情况都会挂起。
         * 还记得我们Task的特性么？ 
         * 在一开始的时候就启动了另一个线程去执行这个Task，当我们调用它的结果的时候如果这个Task已经执行完毕，主线程是不用等待可以直接拿其结果的，如果没有执行完毕那主线程就得挂起等待了。
         */

        /// <summary>
        /// await 实质是在调用awaitable对象的GetResult方法
        /// </summary>
        public async void AwaitTaskMethod()
        {
            Task<string> task = Task.Run(() =>
            {
                Console.WriteLine("另一个线程在运行！");  // 这句话只会被执行一次
                Thread.Sleep(2000);
                return "Hello World";
            });

            // 这里主线程会挂起等待，直到task执行完毕我们拿到返回结果
            var result = task.GetAwaiter().GetResult();
            Console.WriteLine(result);
            // 这里不会挂起等待，因为task已经执行完了，我们可以直接拿到结果
            var result2 = await task;
            Console.WriteLine(result2);
        }


        #endregion

    }
}
