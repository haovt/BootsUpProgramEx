#define FORTEST
using CSharpBasicAndAdvanced;
using System;
using System.Threading.Tasks;


namespace CSharp8OrLater
{
    class Employee {

        public Employee(string visa, RoleLevel level)
        {
            Visa = visa;
            Level = level;
        }

        public void Deconstruct(out string visa, out RoleLevel level) => (visa, level) = (Visa, Level);

        public void Promote()
        {
            Level = GetNextLevel();
        }

        private RoleLevel GetNextLevel()
            => Level switch
            {
                RoleLevel.One => RoleLevel.Two,
                RoleLevel.Two => RoleLevel.Three,
                _ => throw new ArgumentException("Max level reached")
            };

        public string Visa;
        public RoleLevel Level;
    }


    class Program
    {
        public delegate string ConvertedMethod(string value);

        static void Main(string[] args)
        {
            Console.WriteLine("Start!");

            Func<string, string> convertedMethod = s => s + ", Hello!";
            ConvertedMethod convertedMethod2 = s => s + ", Hello!";

            //TestCSharp8();
            //Test3AsyncThread().Wait();
            //TestIndice();
            //TestNullCoalescing();
            //Console.WriteLine("End!");

            // Preprocessor directives
            // DEBUG: build configuration "Debug" or "Release" mode
#if (DEBUG)
            Console.WriteLine("DEBUG is defined");
#elif (!DEBUG)
Console.WriteLine("DEBUG is undefined");
#endif

#if (FORTEST)
            Console.WriteLine("FORTEST is defined");
#elif (!FORTEST)
Console.WriteLine("FORTEST is undefined");
#endif

        }

        static async Task Test3AsyncThread()
        {
            //TestAsynchronousStreams(1, 500);
            //TestAsynchronousStreams(2, 300);
            //await TestAsynchronousStreams(3, 1000);

            //Task t = Task.WhenAll(TestAsynchronousStreams(1, 500), TestAsynchronousStreams(3, 1000));

            //t.Wait();
            //if (t.IsCompleted)
            //{
            //    Console.WriteLine("Task Done!");
            //}
        }

        static void TestCSharp8()
        {
            // Topic 1
            //ApplyRecursivePatternMatching();

            // Topic 2
            // Console apps do not support "asynce void"
            // use async Task and .Wait() to keep 
            TestAsynchronousStreams(1).Wait();
        }

        public static async Task TestAsynchronousStreams(int threadNumber, int delay = 1000)
        {
            // Required: async, await, return IAsyncEnumerable<T>
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine($"Thread {threadNumber}: {number}");
            }
        }

        public static async Task TestAsyncEnumerable()
        {
            var gt = new GenerateText();

            var enumerator = gt.GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                Console.WriteLine($"Read value from enumerator: {enumerator.Current}");
            }
        }

        private static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence(int delay = 1000)
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(delay);
                yield return i;
            }
        }

        // Indice and range
        public static void TestIndice()
        {
            var numbers = new string[]
            {
                "one", // ^10
                "two", // ^9
                "three", // ^8
                "four", // ^7
                "five", // ^6
                "six", // ^5
                "seven", // ^4
                "eight", // ^3
                "nine", // ^2
                "ten" // ^1
            };

            var OneToFive = numbers[1..5]; // two three four five
            var OneToTwo = numbers[0..2]; // one two

            // index from end: ^
            var FourToEight = numbers[^7..^2];
            var TwoToSix = numbers[^9..^4];

            Console.WriteLine(string.Join(',', TwoToSix));

        }

        public static void TestNullCoalescing()
        {
            int? number = null;

            number ??= 1;
            Console.WriteLine(number); // 1

            number ??= 2;
            Console.WriteLine(number); // 2
        }

        // Write a program using recursive pattern matching,
        // a pattern expression applied to output of another pattern expression.
        static void ApplyRecursivePatternMatching()
        {
            // Calculate day off based on visa and level
            Console.WriteLine("Enter your level (1 - 3): ");
            int level = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter visa: ");
            string visa = Console.ReadLine();

            var employee = new Employee(visa, (RoleLevel)level);
            employee.Promote();

            // Positional pattern
            (string currentVisa1, RoleLevel newLevel1) = employee;

            // Tupple pattern
            int days = CalculateDayOff(currentVisa1, newLevel1);
            Console.WriteLine($"Your new day ofss are: {days}");
        }

        // Positional pattern
        static string DescribeSize(Size s) => s switch
        {
            (1, 1, 2) => "1",
            (0, 0, 3) => "0",
        };

        // Switch expression
        static void PrintWeekDaysCase1(WeekDays day)
        {
            switch (day)
            {
                case WeekDays.Monday:
                    Console.WriteLine("Monday");
                    break;
                case WeekDays.Tuesday:
                    Console.WriteLine("Tuesday");
                    break;
                case WeekDays.Wednesday:
                    Console.WriteLine("Wednesday");
                    break;
                case WeekDays.Thursday:
                    Console.WriteLine("Thursday");
                    break;
            }
        }

        // bodies are expression
        static string PrintWeekDaysCase2(WeekDays day) =>
             day switch
             {
                 WeekDays.Monday => "Monday",
                 WeekDays.Tuesday => "Tuesday",
                 WeekDays.Wednesday => throw new NotImplementedException(),
                 WeekDays.Thursday => throw new NotImplementedException(),
                 WeekDays.Friday => throw new NotImplementedException(),
                 WeekDays.Saturday => throw new NotImplementedException(),
                 WeekDays.Sunday => throw new NotImplementedException(),
                 _ => "Nothing"
             };

        //  Tupple pattern
        static int CalculateDayOff(string visa, RoleLevel level) =>
            (level, visa) switch
            {
                (RoleLevel.One, "vha") => 4,
                (RoleLevel.Two, "abc") => 2,
                (RoleLevel.Three, _) => 5,
                _ => 0
            };

        // logical pattern
        //static bool IsNearBy(Employee employee) => employee is { Adress: "HN" };

        // Tupple pattern
        static string CalculateDamage(int attack, int defend) =>
            (attack, defend) switch
            {
                (1, 5) => "Receive 0 damage",
                (4, 2) => "Receive 2 damage",
                (3, 3) => "Receive 0 damage",
            };
    }

    public class Size
    {
        public Size(int h, int w, int d) => (Height, Width, Deep) = (h, w, d);

        public void Deconstruct(out int h, out int w)
            => (h, w) = (Height, Width);

        public void Deconstruct(out int h, out int w, out int d)
                        => (h, w,d ) = (Height, Width, Deep);

        public int Height;
        public int Width;
        public int Deep;
    }

    public enum RoleLevel
    {
        One = 1,
        Two,
        Three
    }

    public enum WeekDays
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
