using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiTroll
{
    class BehaviourManager
    {
        static public Dictionary<int, string> Parse(string fileName)
        {
            Dictionary<int, string> behaviourDictionary = new Dictionary<int, string>();

            StreamReader sr = new StreamReader(fileName);
            do
            {
                string line = sr.ReadLine();
                string[] separatedLine = line.Split(new char[] { ':' }, 2);
                string addrString = separatedLine[0].Trim();
                int addr = 0;
                if (Int32.TryParse(addrString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out addr))
                {
                    string behaviour = separatedLine[1];
                    behaviourDictionary.Add(addr, behaviour);
                }else
                {
                    Console.WriteLine(String.Format("fuck: {0}", addrString));
                }
            } while (sr.Peek() != -1);
            sr.Close();
            sr.Dispose();

            return behaviourDictionary;
        }
    }
}
