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
        /*
         * 
Add this code to rpcblockchain.cpp around line 110

Object blockToJSON(const CBlock& block, const CBlockIndex* blockindex, bool fPrintTransactionDetail)
{
    Object result;
//BEGIN ADDED CODE
char buffer [161];
buffer[159] = 0;
unsigned char *data=(unsigned char*)&(block.nVersion);
for(int j = 0; j < 80; j++)
    sprintf(&buffer[2*j], "%02X", data[j]);
std::string str(buffer);

    result.push_back(Pair("raw", str)); 
//END ADDED CODE
    result.push_back(Pair("hash", block.GetHash().GetHex()));
    int confirmations = -1;

         * */
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
                //scrypted = scrypted.Reverse().ToArray();
                for(int i=32;i>=3;i-=4)
                {
                    //var tmp = scrypted.Reverse().ToArray();
                    var tmp = new byte[4] { scrypted[i - 4], scrypted[i - 3], scrypted[i - 2], scrypted[i - 1] };
                    Console.Write(BitConverter.ToUInt32(tmp, 0).ToString("X8"));
                    //Console.Write(b.ToString("X2"));
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }
    }
}
