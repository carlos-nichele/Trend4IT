using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_to_JSON
{
    class Program
    {

        class Address
        {
            public string line1 { get; set; }
            public string line2 { get; set; }
        }

        class People
        {
            public string name { get; set; }
            public Address address { get; set; }
        }

        static void csvToJson()
        {
            var csv = new List<string[]>();
            var lines = System.IO.File.ReadAllLines(@"C:\TrendIT\Address.csv");

            
            //If CSV doesn't have required columns, put error message and exit
            if (!lines[0].Equals("name,address_line1,address_line2"))
            {
                Console.WriteLine("Invalid CSV file !!!!");
                System.Environment.Exit(0);
            }

            Console.WriteLine("Reading CSV file...");

            // Load CSV file
            int i = 0;
            foreach (string line in lines)
            {

                if (i > 0)
                    csv.Add(line.Split(','));

                ++i;
            }

            // Load class object
            Console.WriteLine("Loading class...");

            var people = new List<People>();

            foreach (var p in csv)
            {

                var person = new People();
                person.name = p[0];
                person.address = new Address() { line1 = p[1], line2 = p[2] };

                people.Add(person);

            }

            Console.WriteLine("Converting class to JSON...");
            // Convert object to JSON
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(people);

            Console.WriteLine(json);
            Console.WriteLine("Press any key to exit...");

            Console.ReadLine();
        }

        static void jsonToCsv()
        {
            Console.WriteLine("Loading JSON file...");

            var lines = System.IO.File.ReadAllText(@"C:\TrendIT\Address.json");

            Console.WriteLine("Deserializing JSON...");

            var people = JsonConvert.DeserializeObject<List<People>>(lines);

            var csv = new StringBuilder();

            csv.AppendLine("name,address_line1,address_line2");

            foreach(var p in people)
            {
                var line = p.name + "," + p.address.line1 + "," + p.address.line2;
                csv.AppendLine(line);
            }

            Console.WriteLine("Generating CSV file...");

            System.IO.File.WriteAllText(@"C:\TrendIT\Address2.CSV", csv.ToString());

            Console.WriteLine("Press any key to exit...");

            Console.ReadLine();

        }

        static void Main(string[] args)
        {

            jsonToCsv();
        }
    }
}
