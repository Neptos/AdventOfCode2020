using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace Common
{
    public static class InputReader
    {
        public static ImmutableList<T> ReadInput<T>(
            string path,
            Func<string, T> lineOperation,
            bool separateOnEmptyLine = false)
        {
            var file = File.OpenText(path);
            var results = new List<T>();
            var outputLine = "";
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (!separateOnEmptyLine)
                {
                    results.Add(lineOperation(line));
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    results.Add(lineOperation(outputLine));
                    outputLine = "";
                    continue;
                }

                outputLine += line;
            }

            if (separateOnEmptyLine)
            {
                results.Add(lineOperation(outputLine));
            }

            return results.ToImmutableList();
        }
    }
}