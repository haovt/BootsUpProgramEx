using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Start!");

            // Exercise 1
            //PrintLINQWay();

            // Topic 2
            //DeferredExecutionPitfall();
            //DeferredStreaming();
            //DeferredNONStreaming();
            //Excercise2();
            //Excercise3();

            // LINQ to XML
            //ReadXmlUsingLinQ();

            //var city = new XElement("city", "Seattle");
            //var customer1 = new XElement("customer", city);
            //var customer2 = new XElement("customer", city);
            //city.SetValue("London");
            //Console.WriteLine(customer2.Element("city").Value);

            //string[] colors = { "green", "brown", "blue", "red" };
            //var list = new List<string>(colors);
            //IEnumerable<string> query = list.Where(c => c.Length == 3);
            //list.Remove("red");

            //Console.WriteLine(query.Count());
        }

        // Write a program to generate a Cartesian Product of three sets.
        static void Excercise2()
        {
            var letters = new[] { "X", "Y" };
            var numbers = new[] { 1, 2 };
            var colours = new[] { "Green", "Orange" };

            var result = letters.Select(l => numbers.Select(n => colours.Select(c =>
            {
                Console.WriteLine($"letter = {l}, number = {n}, colour = {c}");
                return new CartesianProduct(l, n, c);
            }).ToList()).ToList()).ToList();
        }

        // Write a program to display the number and frequency of number from giving array.
        static void Excercise3()
        {
            int[] arr1 = new int[] { 5, 9, 1, 5, 5, 9 };

            var result = arr1.Distinct().Select(x => new NumberAndFrequency(x, arr1.Count(a => a == x)));

            foreach (var r in result)
            {
                Console.WriteLine($"Number {r.Value} appears {r.Frequency} times");
            }
        }

        static void PrintLINQWay()
        {
            var input = new[] { 3, 9, 2, 8, 6, 5 };
            var result = from i in input where i * i > 20 select new SqrNumber
            {
                Number = i,
                SqrNo = i*i
            };

            foreach (var r in result)
            {
                Console.WriteLine($"Number = {r.Number}, SqrNo = {r.SqrNo}");
            }
        }

        static void DeferredExecutionPitfall()
        {
            List<int> dataSource = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> valuesToRemove = null;
            IEnumerable<int> filteredData = new List<int>();

            try
            {
                // null exception??
                filteredData = dataSource.Where(t => !valuesToRemove.All(x => x == t));
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }

            foreach (var i in filteredData) // or?
            {
                Console.WriteLine(i);
            }
        }

        static void DeferredStreaming()
        {
            List<int> dataSource = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Only take item > 5 and end, not read all
            var products = dataSource.Where(x => x > 5);

            // similar
            List<NumberExam> dataSource2 = new List<NumberExam>()
            {
                new NumberExam(7),
                new NumberExam(6),
                new NumberExam(2),
                new NumberExam(3),
                new NumberExam(4),
                new NumberExam(5),
            };

            var products2 = dataSource2.Where(x => x.Data > 2).Take(2);
            Console.WriteLine("Start reading");

            foreach (var p in products2)
            {
                Console.WriteLine($"Read result: {p.Data}");
            }
        }

        static void DeferredNONStreaming()
        {
            List<NumberExam> dataSource2 = new List<NumberExam>()
            {
                new NumberExam(7),
                new NumberExam(6),
                new NumberExam(2),
                new NumberExam(5),
                new NumberExam(3),
                new NumberExam(4),
            };

            var result = dataSource2.OrderBy(x => x.Data);
            foreach (var r in result)
            {
                // Already read all
                string s = "";
            }
        }

        static void ReadXmlUsingLinQ()
        {
            var filename = "PurchaseOrder.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var purchaseOrderFilepath = Path.Combine(currentDirectory, filename);

            XElement purchaseOrder = XElement.Load(purchaseOrderFilepath);

            var adrs = purchaseOrder.Elements("Address").Select(x => x.Element("Street").Value);
            foreach (var a in adrs)
            {
                Console.WriteLine(a);
            }

            var partNos = purchaseOrder.Descendants("Item")
                .Select(x => new  { 
                    Partnumber = (string)x.Attribute("PartNumber"),
                    Price = x.Element("USPrice").Value
                });

            foreach (var p in partNos)
            {
                Console.WriteLine($"{p.Partnumber}: {p.Price}");
            }
        }
    }

    class SqrNumber
    {
        public int Number { get; set; }
        public int SqrNo { get; set; }
    }

    class NumberExam
    {
        private int _value;

        public NumberExam(int data)
        {
            Data = data;
        }

        public int Data
        {
            get
            {
                Console.WriteLine($"Read value: {_value}");
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }

    class CartesianProduct
    {
        public CartesianProduct(string l, int n, string c)
        {
            Letter = l;
            Number = n;
            Colour = c;
        }

        public string Letter { get; set; }
        public int Number { get; set; }
        public string Colour { get; set; }
    }

    class NumberAndFrequency
    {
        public NumberAndFrequency(int v, int f)
        {
            Value = v;
            Frequency = f;
        }

        public int Value { get; set; }
        public int Frequency { get; set; }
    }
}
