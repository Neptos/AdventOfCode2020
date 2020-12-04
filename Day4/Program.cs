using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var inputLines = InputReader.ReadInput("input.txt", s => s);
            var passports = new List<Passport>();
            var passportString = "";
            foreach (var line in inputLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(new Passport(passportString));
                    passportString = "";
                    continue;
                }
                passportString += $" {line}";
            }
            passports.Add(new Passport(passportString));

            var part2Count = passports
                .Where(p => !p.Ignore)
                .Count(p => p.IsValid());
            var part1Count = passports
                .Where(p => !p.Ignore)
                .Count(p => p.NoEmptyValues());
            
            Console.WriteLine($"Part1: {part1Count}");
            Console.WriteLine($"Part2: {part2Count}");
        }
    }
    
    internal class Passport
    {
        public bool Ignore { get; set; }
        public string byr { get; set; }
        public string iyr { get; set; }
        public string eyr { get; set; }
        public string hgt { get; set; }
        public string hcl { get; set; }
        public string ecl { get; set; }
        public string pid { get; set; }
        public string cid { get; set; }
        
        public Passport(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Ignore = true;
            }
            else
            {
                Ignore = false;
                var dataLines = line.Split(" ");
                foreach (var data in dataLines)
                {
                    switch (data.Split(":")[0])
                    {
                        case "byr":
                            byr = data.Split(":")[1];
                            break;
                        case "iyr":
                            iyr = data.Split(":")[1];
                            break;
                        case "eyr":
                            eyr = data.Split(":")[1];
                            break;
                        case "hgt":
                            hgt = data.Split(":")[1];
                            break;
                        case "hcl":
                            hcl = data.Split(":")[1];
                            break;
                        case "ecl":
                            ecl = data.Split(":")[1];
                            break;
                        case "pid":
                            pid = data.Split(":")[1];
                            break;
                        case "cid":
                            cid = data.Split(":")[1];
                            break;
                    }
                }
            }
        }

        public bool IsValid()
        {
            return NoEmptyValues()
                && IsValidYearOfBirth()
                && IsValidIssueYear()
                && IsValidYearOfExpiration()
                && IsValidHeight()
                && IsValidHairColor()
                && IsValidEyeColor()
                && IsValidPassportId();
        }

        private bool IsValidPassportId()
        {
            return pid.All(c => IsNumber(c))
                && pid.Length == 9;
        }

        private static bool IsNumber(char c)
        {
            return c > 47 && c < 58;
        }

        private static bool IsLowerCaseHexLetter(char c)
        {
            return c > 96 && c < 103;
        }

        private bool IsValidEyeColor()
        {
            var validEyeColors = new List<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            return validEyeColors.Contains(ecl);
        }

        private bool IsValidHairColor()
        {
            return hcl.First() == '#'
                && hcl.Skip(1).All(c => IsNumber(c) || IsLowerCaseHexLetter(c))
                && hcl.Length == 7;
        }

        private bool IsValidHeight()
        {
            var heightNumberAsString = hgt.Substring(0, hgt.Length - 2);
            if (!heightNumberAsString.All(IsNumber) || string.IsNullOrWhiteSpace(heightNumberAsString))
            {
                return false;
            }
            var heightNumber = int.Parse(heightNumberAsString);
            var heightUnit = hgt.Substring(hgt.Length - 2);
            return heightUnit == "cm" && heightNumber >= 150 && heightNumber <= 193
                || heightUnit == "in" && heightNumber >= 59 && heightNumber <= 76;
        }

        private bool IsValidYearOfExpiration()
        {
            if (!eyr.All(IsNumber) || string.IsNullOrWhiteSpace(eyr))
            {
                return false;
            }
            var expirationYear = int.Parse(eyr);
            return expirationYear >= 2020 && expirationYear <= 2030;
        }

        private bool IsValidIssueYear()
        {
            if (!iyr.All(IsNumber) || string.IsNullOrWhiteSpace(iyr))
            {
                return false;
            }
            var issueYear = int.Parse(iyr);
            return issueYear >= 2010 && issueYear <= 2020;
        }

        private bool IsValidYearOfBirth()
        {
            if (!byr.All(IsNumber) || string.IsNullOrWhiteSpace(byr))
            {
                return false;
            }
            var yearOfBirth = int.Parse(byr);
            return yearOfBirth >= 1920 && yearOfBirth <= 2002;
        }

        public bool NoEmptyValues()
        {
            return !string.IsNullOrWhiteSpace(byr)
                   && !string.IsNullOrWhiteSpace(iyr)
                   && !string.IsNullOrWhiteSpace(eyr)
                   && !string.IsNullOrWhiteSpace(hgt)
                   && !string.IsNullOrWhiteSpace(hcl)
                   && !string.IsNullOrWhiteSpace(ecl)
                   && !string.IsNullOrWhiteSpace(pid);
        }
    }
}