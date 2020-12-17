using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day16
{
    internal static class Program
    {
        private static void Main()
        {
            var input = InputReader.ReadInput("input.txt", s => s);
            var fields = new List<Field>();
            foreach (var line in input
                .TakeWhile(l => !string.IsNullOrWhiteSpace(l)))
            {
                var rulesPart = line.Split(": ")[1];
                var rule1 = rulesPart.Split(" or ")[0];
                var rule2 = rulesPart.Split(" or ")[1];
                fields.Add(new Field
                {
                    Name = line.Split(":")[0],
                    Rule1 = new Rule
                    {
                        From = int.Parse(rule1.Split("-")[0]),
                        To = int.Parse(rule1.Split("-")[1])
                    },
                    Rule2 = new Rule
                    {
                        From = int.Parse(rule2.Split("-")[0]),
                        To = int.Parse(rule2.Split("-")[1])
                    }
                });
            }

            var myTicket = input
                .SkipWhile(l => !string.IsNullOrWhiteSpace(l))
                .Skip(1)
                .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
                .Skip(1)
                .First()
                .Split(",")
                .Select(int.Parse)
                .ToList();

            var otherTickets = new List<List<int>>();
            foreach (var line in input
                .SkipWhile(l => !string.IsNullOrWhiteSpace(l))
                .Skip(1)
                .SkipWhile(l => !string.IsNullOrWhiteSpace(l))
                .Skip(2))
            {
                otherTickets.Add(line.Split(",").ToList().Select(int.Parse).ToList());
            }

            var timer = new Stopwatch();
            
            timer.Start();
            var ticketScanningErrorRate = GetTicketScanningErrorRate(fields, otherTickets);
            timer.Stop();
            
            Console.WriteLine($"Part1: {ticketScanningErrorRate}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
            
            timer.Restart();
            var validOtherTickets = GetValidOtherTickets(fields, otherTickets);
            var validPermutation = GetFieldsOrder(fields, validOtherTickets, new List<Field>());

            long departureSum = 1;
            for (var i = 0; i < fields.Count; i++)
            {
                if (fields[i].Name.StartsWith("departure"))
                {
                    departureSum *= myTicket[validPermutation[i]];
                }
            }
            timer.Stop();

            Console.WriteLine($"Part1: {departureSum}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
        }

        private static List<int> GetFieldsOrder(
            List<Field> fields,
            List<List<int>> tickets,
            List<Field> removedFields,
            int column = 0)
        {
            var returnListList = new List<List<int>>();
            for (var i = 0; i < fields.Count; i++)
            {
                bool all = true;
                foreach (var ticketFields in tickets)
                {
                    if (!fields[i].MatchesAnyRule(ticketFields[column]))
                    {
                        all = false;
                        break;
                    }
                }

                if (!removedFields.Contains(fields[i]) && all)
                {
                    var newList = new List<int> {i};
                    var newRemovedFields = removedFields.ToList();
                    newRemovedFields.Add(fields[i]);
                    newList.AddRange(GetFieldsOrder(fields, tickets, newRemovedFields, column+1));
                    returnListList.Add(newList);
                }
            }
            
            var returnList = returnListList.FirstOrDefault(l => l.Count == returnListList.Max(l2 => l2.Count));

            return returnList ?? new List<int>();
        }

        private static List<List<int>> GetValidOtherTickets(List<Field> fields, List<List<int>> otherTickets)
        {
            var validTickets = new List<List<int>>();
            foreach (var otherTicket in otherTickets)
            {
                if (GetTicketScanningErrorRate(fields, new List<List<int>> {otherTicket}) == 0)
                {
                    validTickets.Add(otherTicket);
                }
            }

            return validTickets;
        }

        private static int GetTicketScanningErrorRate(List<Field> fields, List<List<int>> otherTickets)
        {
            var errorRate = 0;
            foreach (var otherTicket in otherTickets)
            {
                foreach (var ticketField in otherTicket)
                {
                    var any = false;
                    foreach (var field in fields)
                    {
                        if (field.MatchesAnyRule(ticketField))
                        {
                            any = true;
                            break;
                        }
                    }

                    if (!any)
                    {
                        errorRate += ticketField;
                    }
                }
            }

            return errorRate;
        }

        internal class Field
        {
            public string Name { get; set; }
            public Rule Rule1 { get; set; }
            public Rule Rule2 { get; set; }

            public bool MatchesAnyRule(int value)
            {
                return Rule1.IsInRange(value) || Rule2.IsInRange(value);
            }
        }
        
        internal class Rule
        {
            public int From { get; set; }
            public int To { get; set; }

            public bool IsInRange(int value)
            {
                return value >= From && value <= To;
            }
        }
    }
}