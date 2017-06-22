# SimpleSequence [![Build status](https://ci.appveyor.com/api/projects/status/3dftm9x1rvfjusk0?svg=true)](https://ci.appveyor.com/project/trashvin/simplesequence)
Is a custom sequence generator. Supports InMemory , Text File and MSSQL storage. Developers can also create their own storage providers.

## Usage
It can be used to generate a custom ID using a pre-defined and configurable format.

### Sequence Configuration

Properties :

1. Fomat - The generated string format. ( see Format Examples )
2. ApplicationName - Optional setting storing the name of the application using it.
3. SequenceLenght - The lenght of the generated sequence number.
4. FillerCharacter - The characer used to pad the generated sequence
5. Date - If needed, the date to be used by the Generator. The default is current system date.

Sample Usage in Code :
```C#
SequenceConfiguration conf = new SequenceConfiguration()
{
  Format = "N",
  ApplicationName = "Test App",
  SequenceLenght = 5,
  Date = DateTime.Now
};

Generator gen = new Generator(conf);
```

### Storage Providers
Storage providers provides the persistence object for the sequence. The library provides 3 simple storage providers :
1. InMemoryStorage - the sequence is only stored in memory.
```C#
InMemoryStorage storage1 = new InMemoryStorage(); // Sequence starts at 1
InMemoryStorage storage2 = new InMemoryStorage(100); // Sequence starts at 100

Generator gen = new Generator(storage1, conf);
```
2. TextFileStorage - the sequence will be written on a text file in clear text.
```C#
TextFileStorage fileStorage1 = new TextFileStorage("C:\\Data\\Example.id"); // Starts at 1 , storage at filename provided
TextFileStorage fileStorage1 = new TextFileStorage("C:\\Data\\Example.id",10); // Starts at 10
```
3. MSSQLStorage - the sequence is stored in MSSQL database. The table SIMPLEIDSEQUENCE with 1 field [id] must be created else the sequence returned will alwys be 1 or the starting count set at constructor.
```C#
MSSQLStorage _msSqlStorage = new MSSQLStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True");
MSSQLStorage _msSqlStorage = new MSSQLStorage(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True",50);
```

### Generated Sequence Format Examples
Format Notations :
1. N - the sequence number
2. Y - System Year ( 4 digits)
3. M - System Month (2 Digits )
4. D - System Day ( 2 Digits )
5. 0...9 - Custom Values to be passed on the GenerateID call
6. {....} - Any value in between brackets will be included as is.

Example: (Assume sequence = 00003 and custom values = {"AAA","BBB","CCC"}
1. N - "00001"
2. 0N1 - "AAA00001BBB"
3. Y-N{TEST} - "2017-00005TEST"

Sample Code
```C#
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
            Console.WriteLine(String.Format("Format : YN{-TEST}  ;  Result : {0}", generate.GenerateID()));

            //0N1
            config.Format = "0N1";
            Console.WriteLine(String.Format("Format : 0N1  ;  Result : {0}", generate.GenerateID(customs)));
            Console.Read();
        }

        
        //Output:
        //Format : N  ;  Result : 00001
        //Format : YN{-TEST}  ;  Result : 201700002-TEST
        //Format : 0N1  ;  Result : XXXX100003Y200

```

## Minimum Target Framework 
- .NET Framework 4.5
