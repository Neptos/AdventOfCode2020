using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Common;

namespace Day17
{
    internal static class Program
    {
        private static void Main()
        {
            var input = InputReader.ReadInput("input.txt", s => s);
            Part1(input);
            Part2(input);
        }

        private static void Part2(ImmutableList<string> input)
        {
            var world = new World4d();
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    world.GetCubeAt(x, y, 0, 0).Active = input[y][x] == '#';
                }
            }

            var timer = new Stopwatch();

            timer.Start();
            world.ExpandWorld();
            for (var i = 0; i < 6; i++)
            {
                world.ExpandWorld();
                var operations = new List<Operation>();
                foreach (var (x, xDictionary) in world.Cubes)
                {
                    if (x == world.Cubes.Keys.Max() || x == world.Cubes.Keys.Min())
                    {
                        continue;
                    }

                    foreach (var (y, yDictionary) in xDictionary)
                    {
                        if (y == world.Cubes[0].Keys.Max() || y == world.Cubes[0].Keys.Min())
                        {
                            continue;
                        }

                        foreach (var (z, zDictionary) in yDictionary)
                        {
                            if (z == world.Cubes[0][0].Keys.Max() || z == world.Cubes[0][0].Keys.Min())
                            {
                                continue;
                            }

                            foreach (var (w, cube) in zDictionary)
                            {
                                if (w == world.Cubes[0][0][0].Keys.Max() || w == world.Cubes[0][0][0].Keys.Min())
                                {
                                    continue;
                                }

                                if (cube.Active)
                                {
                                    var nrActiveNeighbors = cube.GetActiveNeighborsCount(world);
                                    if (nrActiveNeighbors != 2 && nrActiveNeighbors != 3)
                                    {
                                        operations.Add(new Operation(cube));
                                    }
                                }
                                else
                                {
                                    var nrActiveNeighbors = cube.GetActiveNeighborsCount(world);
                                    if (nrActiveNeighbors == 3)
                                    {
                                        operations.Add(new Operation(cube));
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var operation in operations)
                {
                    operation.Execute();
                }
            }

            var remainingActiveCubes = 0;
            foreach (var (_, xDimension) in world.Cubes)
            {
                foreach (var (_, yDimension) in xDimension)
                {
                    foreach (var (_, zDimension) in yDimension)
                    {
                        foreach (var (_, cube) in zDimension)
                        {
                            if (cube.Active)
                            {
                                remainingActiveCubes++;
                            }
                        }
                    }
                }
            }

            timer.Stop();

            Console.WriteLine($"Part2: {remainingActiveCubes}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
        }

        private static void Part1(ImmutableList<string> input)
        {
            var world = new World();
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    world.GetCubeAt(x, y, 0).Active = input[y][x] == '#';
                }
            }

            var timer = new Stopwatch();

            timer.Start();
            world.ExpandWorld();
            for (var i = 0; i < 6; i++)
            {
                world.ExpandWorld();
                var operations = new List<Operation>();
                foreach (var (x, xDictionary) in world.Cubes)
                {
                    if (x == world.Cubes.Keys.Max() || x == world.Cubes.Keys.Min())
                    {
                        continue;
                    }

                    foreach (var (y, yDictionary) in xDictionary)
                    {
                        if (y == world.Cubes[0].Keys.Max() || y == world.Cubes[0].Keys.Min())
                        {
                            continue;
                        }

                        foreach (var (z, cube) in yDictionary)
                        {
                            if (z == world.Cubes[0][0].Keys.Max() || z == world.Cubes[0][0].Keys.Min())
                            {
                                continue;
                            }

                            if (cube.Active)
                            {
                                var nrActiveNeighbors = cube.GetActiveNeighborsCount(world);
                                if (nrActiveNeighbors != 2 && nrActiveNeighbors != 3)
                                {
                                    operations.Add(new Operation(cube));
                                }
                            }
                            else
                            {
                                var nrActiveNeighbors = cube.GetActiveNeighborsCount(world);
                                if (nrActiveNeighbors == 3)
                                {
                                    operations.Add(new Operation(cube));
                                }
                            }
                        }
                    }
                }

                foreach (var operation in operations)
                {
                    operation.Execute();
                }
            }

            var remainingActiveCubes = 0;
            foreach (var (_, xDimension) in world.Cubes)
            {
                foreach (var (_, yDimension) in xDimension)
                {
                    foreach (var (_, cube) in yDimension)
                    {
                        if (cube.Active)
                        {
                            remainingActiveCubes++;
                        }
                    }
                }
            }

            timer.Stop();

            Console.WriteLine($"Part1: {remainingActiveCubes}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
        }
    }
}