using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleSequence;
using SimpleSequence.StorageProvider;

namespace SimpleSequence.Example
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string appName = "Test App";
            int seqLen = 5;
            char filler = '0';
            InMemoryStorage inMemorySource = new InMemoryStorage();
            Generator generate;
            SequenceConfiguration config;

            string[] customs = null;
            string[] formats = {"N","MDY-N-{ABC}","0N","0:YN-1"};
            int value1 = 1;
            int value2 = 200;

            config = new SequenceConfiguration()
            {
                Format = "N",
                ApplicationName = appName,
                SequenceLenght = seqLen,
                FillerCharacter = filler,
                Date = DateTime.Now
            };

            string temp1 = value1.ToString().PadLeft(5, 'X');
            string temp2 = value2.ToString().PadLeft(4, 'Y');

            customs = new string[] { temp1, temp2 };

            //N
            generate = new Generator(config);
            Console.WriteLine(String.Format("Format : N  ;  Result : {0}", generate.GenerateID()));

            //YN{-TEST}
            config.Format = "YN{-TEST}";
            Console.WriteLine(String.Format("Format : N  ;  Result : {0}", generate.GenerateID()));

            //0N1
            config.Format = "0N1";
            Console.WriteLine(String.Format("Format : N  ;  Result : {0}", generate.GenerateID(customs)));



            Console.Read();
        }

    }
}
