using System;
using System.Threading;
using System.Threading.Tasks;

namespace threads
{
    class Program
    {
        static void Main(string[] args)
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(()=>{
                int i = 0;
                while(true){
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++} \t");
                    Thread.Sleep(1000);
                }
            },paranoid.Token);

            Console.ReadKey();
            planned.Cancel();

            Console.WriteLine("Program ended");
            Console.ReadKey();
            
        }
    }
}
