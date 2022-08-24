using Nito.AsyncEx;
using System.Diagnostics;

namespace AsyncExamples;

class Program
{
    static void Main()
    {
        AsyncContext.Run(async () => await MainAsync());
    }

    public static async Task MainAsync()
    {
        var numbers = Enumerable.Range(1, 10);

        Example1();
        Console.WriteLine("I am just in the middle...........");
        Example1();

        //await Example2();
        //Console.WriteLine("I am just in the middle...........");
        //await Example3();

        //Example2();
        //Console.WriteLine("I am just in the middle...........");
        //await Example3();

        //var stopwatch = new Stopwatch();
        //stopwatch.Start();
        //var task1 = Example2();
        //Console.WriteLine("I am just in the middle of 2 and 3...........");
        //var task2 = Example3();
        //await Task.WhenAll(task1, task2);
        //stopwatch.Stop();
        //Console.ForegroundColor = ConsoleColor.White;
        //Console.WriteLine($"Examples 2 and 3 ended after {stopwatch.Elapsed:s\\.fff} seconds.");
        //Console.ResetColor();

        //await Example4();

        int GetRandomMilliseconds()
        {
            return new Random().Next(100, 500);
        }

        decimal PrintNumber(int number)
        {
            var milliseconds = GetRandomMilliseconds();
            var seconds = Math.Round(Convert.ToDecimal(milliseconds) / 1000, 4);
            Thread.Sleep(milliseconds);
            Console.WriteLine($"--- {number} -------------- (waiting {seconds} seconds)");
            return seconds;
        }

        async Task<decimal> PrintNumberAsync(int number)
        {
            var milliseconds = GetRandomMilliseconds();
            var seconds = Math.Round(Convert.ToDecimal(milliseconds) / 1000, 4);
            await Task.Delay(milliseconds);
            Console.WriteLine($"--- {number} -------------- (waiting {seconds} seconds)");
            return await Task.FromResult(seconds);
        }

        async Task ExecuteAsync(int exampleNumber, Func<Task> function)
        {
            var stopwatch = new Stopwatch();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Example {exampleNumber} has started.");
            Console.ResetColor();
            stopwatch.Start();
            await function();
            stopwatch.Stop();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Example {exampleNumber} ended after {stopwatch.Elapsed:s\\.fff} seconds.");
            Console.ResetColor();
            Console.WriteLine();
        }

        void Example1()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Example 1 has started.");
            Console.ResetColor();
            decimal totalSeconds = 0;
            foreach (int i in numbers)
            {
                totalSeconds += PrintNumber(i);
            }
            stopwatch.Stop();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Total: {totalSeconds} seconds");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Time elapsed: " + stopwatch.Elapsed.ToString(@"s\.fff"));
            Console.ResetColor();
        }

        async Task Example2()
        {
            var function = async () =>
            {
                decimal totalSeconds = 0;
                foreach (int i in numbers)
                {
                    totalSeconds += await PrintNumberAsync(i);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Example 2 Total: {totalSeconds} seconds");
                Console.ResetColor();
            };
            await ExecuteAsync(2, function);
        }

        async Task Example3()
        {
            var function = async () =>
            {
                decimal totalSeconds = 0;
                foreach (int i in numbers)
                {
                    totalSeconds += await PrintNumberAsync(i);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Example 3 Total: {totalSeconds} seconds");
                Console.ResetColor();
            };
            await ExecuteAsync(3, function);
        }

        async Task Example4()
        {
            var function = async () =>
            {
                decimal totalSeconds = 0;
                var tasks = numbers.Select(i => PrintNumberAsync(i));
                var results = await Task.WhenAll(tasks);
                foreach (var result in results)
                {
                    totalSeconds += result;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Example 4 Total: {totalSeconds} seconds");
                Console.ResetColor();
            };
            await ExecuteAsync(4, function);
        }
    }
}

