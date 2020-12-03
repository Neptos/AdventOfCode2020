using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var passwords = InputReader.ReadInput("Passwords.txt", LineOperation);

            var timer = new Stopwatch();
            timer.Start();
            var nrValidPasswordsPart1 = FirstPart(passwords);
            timer.Stop();
            var elapsedPart1 = timer.Elapsed;

            timer.Restart();
            var nrValidPasswordsPart2 = SecondPart(passwords);
            timer.Stop();
            
            Console.WriteLine($"Part1 time: {elapsedPart1}");
            Console.WriteLine($"Part2 time: {timer.Elapsed}");
            Console.WriteLine($"Part 1: {nrValidPasswordsPart1}");
            Console.WriteLine($"Part 2: {nrValidPasswordsPart2}");
        }

        private static Password LineOperation(string line)
        {
            var requirements = line.Split(": ")[0];
            var pass = line.Split(": ")[1];
            return new Password
            {
                Pass = pass,
                LowerBound = int.Parse(requirements.Split('-')[0]),
                UpperBound = int.Parse(requirements.Split('-')[1].Split(' ')[0]),
                RequiredLetter = requirements.Split(' ')[1][0]
            };
        }

        private static int SecondPart(ImmutableList<Password> passwords)
        {
            var nrValidPasswordsPart2 = 0;
            foreach (var password in passwords)
            {
                if ((password.Pass[password.UpperBound - 1] == password.RequiredLetter ||
                     password.Pass[password.LowerBound - 1] == password.RequiredLetter) &&
                    !(password.Pass[password.UpperBound - 1] == password.RequiredLetter &&
                      password.Pass[password.LowerBound - 1] == password.RequiredLetter))
                {
                    nrValidPasswordsPart2++;
                }
            }

            return nrValidPasswordsPart2;
        }

        private static int FirstPart(ImmutableList<Password> passwords)
        {
            var nrValidPasswordsPart1 = 0;
            foreach (var password in passwords)
            {
                var nrOfRequiredLetters = password.Pass.Count(p => p == password.RequiredLetter);
                if (nrOfRequiredLetters >= password.LowerBound && nrOfRequiredLetters <= password.UpperBound)
                {
                    nrValidPasswordsPart1++;
                }
            }

            return nrValidPasswordsPart1;
        }

        private class Password
        {
            public string Pass { get; set; }
            public int UpperBound { get; set; }
            public int LowerBound { get; set; }
            public char RequiredLetter { get; set; }
        }
    }
}