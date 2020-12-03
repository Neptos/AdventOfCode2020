using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace Common
{
    public static class InputReader
    {
        public static ImmutableList<T> ReadInput<T>(string path, Func<string, T> lineOperation)
        {
            var file = File.OpenText(path);
            var results = new List<T>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                results.Add(lineOperation.Invoke(line));
            }

            return results.ToImmutableList();
        }
    }
}