using System;

namespace src
{
    class Program
    {
        static int Main(bool build)
        {
            Console.WriteLine("Hello World!");
            if (build)
            {
                Console.WriteLine("Running in verbose mode");
            }
            Console.ReadKey();

            return 0;
        }
    }
}
