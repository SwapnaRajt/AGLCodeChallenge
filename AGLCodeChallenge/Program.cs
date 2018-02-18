using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ProcessJson;

namespace AGLCodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter service URL");
            string requestUri = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(requestUri))
            {
                var process = new ProcessJson();
                process.DisplayPets(requestUri);
            }
            Console.ReadLine();
            Environment.Exit(0);

        }

        
     }
}
