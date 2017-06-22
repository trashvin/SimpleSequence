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
        private static string _appName = "Test App";
        private static int _seqLen = 5;
        private static char _filler = '0';
        private static TextFileStorage _fileSource = new TextFileStorage("C:\\Data\\Example.id");
        private static InMemoryStorage _inMemorySource = new InMemoryStorage();
        private static MSSQLStorage _msSqlStorage = new MSSQLStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True");


        static void Main(string[] args)
        {
            string[] customs = null;
            string[] formats = {"N","MDY-N-{ABC}","0N","0:YN-1"};
            int value1 = 1;
            int value2 = 200;
            
            Console.WriteLine("Starting Async Call");

            foreach(var format in formats)
            {
                string temp1 = value1.ToString().PadLeft(5, '*');
                string temp2 = value2.ToString().PadLeft(4, '#');
                customs = new string[]{ temp1,temp2};
                Task<string> result = GenerateIDAsync(format, customs);
                result.Wait();
                Console.WriteLine(string.Format("Generated ID [{0}]: {1}", format, result.Result));
                value1++;
                value2++;
            }

            Console.WriteLine("Starting Sync Call");

            foreach(var format in formats)
            {
                string temp1 = value1.ToString().PadLeft(5, '*');
                string temp2 = value2.ToString().PadLeft(4, '#');
                customs = new string[]{ temp1,temp2};

                string result = GenerateID(format, customs);
                Console.WriteLine(string.Format("Generated ID [{0}]: {1}", format, result));

                value1++;
                value2++;
            }


            Console.Read();
        }

        private static async Task<string> GenerateIDAsync(string format, string[] customValues = null)
        {
            SequenceConfiguration config = new SequenceConfiguration()
            {
                IDFormat = format,
                ApplicationName = _appName,
                SequenceLenght = _seqLen,
                FillerCharacter = _filler
            };

            Generator generate = new Generator(_inMemorySource,config);

            Task<string> id = Task.Run(() => generate.GenerateID(customValues));
                    
            return await id;
        }

        private static string GenerateID(string format, string[] customValues = null)
        {
            SequenceConfiguration config = new SequenceConfiguration()
            {
                IDFormat = format,
                ApplicationName = _appName,
                SequenceLenght = _seqLen,
                FillerCharacter = _filler
            };

            Generator generate = new Generator(_inMemorySource,config);

            string id = generate.GenerateID(customValues);

            return id;
        }

    }
}
