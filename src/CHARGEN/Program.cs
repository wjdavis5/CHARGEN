using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using CHARGEN.Interfaces;
using CHARGEN.Models;

namespace CHARGEN
{
    public class Program
    {
        private static short? _port;
        private static string _chargenType = "Standard";
        private static IChargenService _chargenService;
        public static void Main(string[] args)
        {
            if (args.Contains("--Port"))
            {
                var x = args.ToList().IndexOf("--Port");
                var rawValue = args[x + 1];
                _port = Convert.ToInt16(rawValue);
            }

            if (args.Contains("--CharGen"))
            {
                var x = args.ToList().IndexOf("--CharGen");
                var rawValue = args[x + 1];
                _chargenType = rawValue;
            }

            switch (_chargenType)
            {
                case "Random":
                    Console.WriteLine("Using Random Chargen Service on Port {0}", _port ?? 19);
                    _chargenService = new TcpChargenService(new RandomCharacterGenerator(), _port ?? 19);
                    break;

                case "Standard":
                default:
                    Console.WriteLine("Using Standard Chargen Service on Port {0}",_port ?? 19);
                    _chargenService = new TcpChargenService(new StandardCharacterGenerator(), _port ?? 19);
                    break;

            }

            _chargenService.Listen();
            Console.WriteLine("CHARGEN is running - CTRL+C to quit");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
