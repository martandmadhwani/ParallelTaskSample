using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParellalTaskSample
{
    class Program
    {
        public static void PrintLabel(dynamic label)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write((string)label);
            }
        }

        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            //Create new task
            Task sampleTask = new Task(() => PrintLabel("A"));
            sampleTask.Start();

            //we can create new task thread using task.factory
            var taskTwo = Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    PrintLabel("B");
                }
            },token
            );

            //wait till task to be completed
            Task.WaitAll(sampleTask, taskTwo);

            //wait till task to be completed
            Task.WaitAll(sampleTask, taskTwo);

        }
    }
}
