using Replicon.Cryptography.SCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashChecker
{
    class Program
    {
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        static void Main(string[] args)
        {
            while (true)
            {
                string arg = Console.ReadLine();
                var data = StringToByteArray(arg.Substring(0, 160));
                // data = data.Reverse().ToArray();
                if (data.Count() != 80) throw new ApplicationException();
                var scrypted = SCrypt.DeriveKey(data, data, 1024, 1, 1, 32);
                foreach (var b in scrypted)
                {
                    Console.Write(b.ToString("X2"));
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }
}
