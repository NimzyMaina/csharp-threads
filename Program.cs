using System;
using System.Threading;
using System.Threading.Tasks;

namespace threads
{
    class Program
    {
        static void Main(string[] args)
        {
            // 
            // declear cancellation token source
            var cts = new CancellationTokenSource();
            // get cancellation token
            var token = cts.Token;

            // declare task to run in a different thread
            var t = new Task(() => {
                int i = 0;
                while(true)
                {
                    if(token.IsCancellationRequested){
                        // Show thread Id
                        Console.WriteLine($"The task was running on thead ID ===> {Task.CurrentId}");
                        throw new OperationCanceledException();
                        // or ---> break;
                    }
                    // or instead of if statement call below method (Recommended way)
                    // token.ThrowIfCancellationRequested();
                    else
                        Console.Write($"{i++} \t");
                }
            },token);
            t.Start();

            // Use this if you want to cleanup after the task has finished executing completely
            Task.Factory.StartNew(() => {
                // Blocking call
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle has been release");
            });

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine($"Main program has ended on  thread {Task.CurrentId}");
            Console.WriteLine($"Task status {t.Status.ToString()}");
            Console.ReadKey();
        }
    }
}
