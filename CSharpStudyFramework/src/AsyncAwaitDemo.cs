using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy
{
    public class AsyncAwaitDemo
    {
        public async Task mainThreadFunc1_callLongRunTask_MainThreadWillNotBlockAtThisFunc()
        {
            int result = await LongRunningOperationAsync();
            //该函数被block,但是主线程会继续执行后面的函数，longRunTask结束后执行该函数后面的语句
            Console.WriteLine(result);
        }

        public void mainThreaddFunc2_WillNotBlockByFunc1()
        {
            Console.WriteLine("other function");
        }

        public async Task<int> LongRunningOperationAsync()
        {
            Console.WriteLine("will sleep");
            await Task.Delay(10000);
            Console.WriteLine("sleep done");
            return 19;
        }



        //another example
        //https://www.c-sharpcorner.com/article/async-and-await-in-c-sharp/
        public async void callMethod()
        {
            Task<int> task = Method1();
            Method2();
            int count = await task;
            Method3(count);
        }

        public static async Task<int> Method1()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(" Method 1");
                    count += 1;
                }
            });
            return count;
        }

        public static void Method2()
        {
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(" Method 2");
            }
        }

        public static void Method3(int count)
        {
            Console.WriteLine("Total count is " + count);
        }
    }
}
