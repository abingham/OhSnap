using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AOLoader;

namespace AOLoaderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var c in AOLoader.AOLoader.loadFile(args[0]))
            {
                Console.WriteLine(c.Code);
            }

            Console.ReadLine();
        }
    }
}
