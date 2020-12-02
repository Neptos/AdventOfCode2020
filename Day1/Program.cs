using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var expenses = LoadExpenses();
            
            var timer = new Stopwatch();
            timer.Start();
            var result1 = FirstPart(expenses);
            timer.Stop();
            var elapsedPart1 = timer.Elapsed;
            
            timer.Restart();
            var result2 = SecondPart(expenses);
            timer.Stop();

            Console.WriteLine($"Part1 time: {elapsedPart1}");
            Console.WriteLine($"Part2 time: {timer.Elapsed}");
            Console.WriteLine($"Part1 result: {result1}");
            Console.WriteLine($"Part2 result: {result2}");
        }

        private static ImmutableList<int> LoadExpenses()
        {
            var expensesFile = File.OpenText("expenses.txt");
            var expenses = new List<int>();
            string line;
            while ((line = expensesFile.ReadLine()) != null)
            {
                expenses.Add(int.Parse(line));
            }

            return expenses.ToImmutableList();
        }

        private static int SecondPart(ImmutableList<int> expenses)
        {
            var expensesCopy = new List<int>(expenses);
            foreach (var expense1 in expenses)
            {
                expensesCopy.Remove(expense1);
                foreach (var expense2 in expensesCopy)
                {
                    var expense3 = expensesCopy.FirstOrDefault(e => expense1 + expense2 + e == 2020);
                    if (expense3 == 0) continue;
                    return expense1 * expense2 * expense3;
                }
            }

            return 0;
        }

        private static int FirstPart(ImmutableList<int> expenses)
        {
            var expensesCopy = new List<int>(expenses);
            foreach (var expense in expenses)
            {
                expensesCopy.Remove(expense);
                var found = expensesCopy.FirstOrDefault(e => e + expense == 2020);
                if (found == 0) continue;
                return found * expense;
            }

            return 0;
        }
    }
}
