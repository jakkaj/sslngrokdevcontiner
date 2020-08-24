using System;

namespace src
{
    class Program
    {
        static int Main(bool verbose)
        {
            Console.WriteLine("Hello World!");
            if (verbose)
            {
                Console.WriteLine("Running in verbose mode");
            }
            Console.ReadKey();

            return 0;
        }
    }
}
