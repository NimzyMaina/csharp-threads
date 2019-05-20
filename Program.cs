using System;
using System.Threading.Tasks;

namespace threads
{
    class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while( i-- > 0)
            {
                Console.Write(c);
            }
        }

        // Static method takes object param (Can be cast to an object)
        public static void Write(object o)
        { 
            // Cast to an object
            // var x = (ExampleClass) 0;
            int i = 1000;
            while( i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with ID {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // Example 1
            Task.Factory.StartNew(() => Write('r'));
            
            // Example 2
            Task.Factory.StartNew(Write, 123);
            
            // Example 3
            var t = new Task(() => Write('?'));
            t.Start();

            // Example 4
            var t2 = new Task(Write, "Hello");
            t2.Start();

            Write('-');

            var t3 = new Task<int>(TextLength, "Hello Nigga!!!");
            t3.Start();

            var res = TextLength("Hello Nigga!!!");
            Console.WriteLine($"\nThe length is {res}");

            Task<int> t4 = Task.Factory.StartNew(TextLength,"Hello Nigga v2.0");
            // Or ---> Task<int> t4 = Task.Factory.StartNew<int>(TextLength,"Hello Nigga v2.0");
            Console.WriteLine($"\nThe length for v2.0 is {t4.Result}");

            Console.ReadKey();
        }
    }
}
