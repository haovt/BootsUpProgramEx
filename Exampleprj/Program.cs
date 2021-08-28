using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CSharpBasicAndAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            // Topic 1: "using" statements
            // ReadAndExportToTxtFile();

            // Topic 2: Reflection
            // ShowMetadataOfStudent();

            // Topic 2: Dynamically load an assembly
            // Locates a type from this assembly and creates an instance of it using the system activator.
            // LoadAssembly();
        }

        public static void LoadAssembly()
        {
            var assembly = typeof(Student).Assembly;
            // Create instance using default constructor
            var student = assembly.CreateInstance("CSharpBasicAndAdvanced.Student");
            Console.WriteLine($"{student}");
        }

        public static void ShowMetadataOfStudent()
        {
            var studentObj = new Student();

            // System.Object.GetType()
            var typ1 = studentObj.GetType();
            Console.WriteLine("Metadata:");
            Console.WriteLine($"Class: {typ1.Name}");

            Console.WriteLine("==========");
            var sb = new StringBuilder();
            var properties = typ1.GetProperties();
            foreach (var p in properties)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(p.Name);
            }

            Console.WriteLine($"Properties: {sb}");

            Console.WriteLine("==========");
            Console.WriteLine("Methods: ");
            var methods = typ1.GetMethods();
            foreach (var m in methods)
            {
                Console.Write($"{m.Name}: ");
                var parameters = m.GetParameters();
                foreach (var p in parameters)
                {
                    Console.Write($"{p.Name} {p.ParameterType}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("==========");
            Console.WriteLine("Constructors: ");
            var constructors = typ1.GetConstructors();
            foreach (var c in constructors)
            {
                Console.Write($"{c.Name}: ");
                var parameters = c.GetParameters();
                foreach (var p in parameters)
                {
                    Console.Write($"{p.Name} {p.ParameterType} ");
                }

                Console.WriteLine();
            }

            // System.Type.GetType() = typeof(Student)
            //var typ2 = Type.GetType("CSharpBasicAndAdvanced.Student");
            //Console.WriteLine(typ2);
        }

        public static void ReadAndExportToTxtFile()
        {
            string connectionString = GetConnectionString();
            var lines = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string oString = "Select * from ABC";
                SqlCommand oCmd = new SqlCommand(oString, connection);
                connection.Open();

                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        string a2Text = oReader["A2"].ToString();
                        lines.Add(a2Text);
                    }
                    connection.Close();
                }
            }

            using (StreamWriter file = new("WriteLines2.txt"))
            {
                file.Write(string.Join(';', lines));
            }
        }

        static private string GetConnectionString()
        {
            return "Data Source=NB26194\\SQLEXPRESS;Initial Catalog=ExampleSchema;"
                + "Integrated Security=true;";
        }
    }
}
