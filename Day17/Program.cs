using System;
using System.Collections.Generic;
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
            ExpandWorld(world);
            for (var i = 0; i < 6; i++)
            {
                ExpandWorld(world);
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
            
            PrintCurrentState(world);
            
            Console.WriteLine($"Part1: {remainingActiveCubes}");
            Console.WriteLine($"Timer: {timer.Elapsed}");
        }

        private static void PrintCurrentState(World world)
        {
            Console.WriteLine("--------");
            for (var j = world.Cubes[0][0].Keys.Min(); j <= world.Cubes[0][0].Keys.Max(); j++)
            {
                for (var k = world.Cubes[0].Keys.Min(); k <= world.Cubes[0].Keys.Max(); k++)
                {
                    var cubeLine = "";
                    for (var l = world.Cubes.Keys.Min(); l <= world.Cubes.Keys.Max(); l++)
                    {
                        cubeLine += world.Cubes[l][k][j].ToString();
                    }

                    Console.WriteLine(cubeLine);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("--------");
        }

        private static void ExpandWorld(World world)
        {
            var xMax = world.Cubes.Keys.Max();
            var xMin = world.Cubes.Keys.Min();
            var yMax = world.Cubes[0].Keys.Max();
            var yMin = world.Cubes[0].Keys.Min();
            var zMax = world.Cubes[0][0].Keys.Max();
            var zMin = world.Cubes[0][0].Keys.Min();
            for (var x = xMin-1; x <= xMax+1; x++)
            {
                for (var y = yMin-1; y <= yMax+1; y++)
                {
                    for (var z = zMin-1; z <= zMax+1; z++)
                    {
                        world.GetCubeAt(x, y, z);
                        world.GetCubeAt(x, y, z);
                    }
                }
            }
        }

        private class Operation
        {
            private Cube Cube { get; }

            public Operation(Cube cube)
            {
                Cube = cube;
            }

            public void Execute()
            {
                Cube.Active = !Cube.Active;
            }
        }
        
        private class World
        {
            public Dictionary<int, Dictionary<int, Dictionary<int, Cube>>> Cubes { get; }

            public World()
            {
                Cubes = new Dictionary<int, Dictionary<int, Dictionary<int, Cube>>>();
            }

            public Cube GetCubeAt(int x, int y, int z)
            {
                if (Cubes.ContainsKey(x))
                {
                    if (Cubes[x].ContainsKey(y))
                    {
                        if (!Cubes[x][y].ContainsKey(z))
                        {
                            Cubes[x][y][z] = new Cube(x, y, z);
                        }
                    }
                    else
                    {
                        Cubes[x][y] = new Dictionary<int, Cube>();
                        Cubes[x][y][z] = new Cube(x, y, z);
                    }
                }
                else
                {
                    Cubes[x] = new Dictionary<int, Dictionary<int, Cube>>();
                    Cubes[x][y] = new Dictionary<int, Cube>();
                    Cubes[x][y][z] = new Cube(x, y, z);
                }

                return Cubes[x][y][z];
            }
        }
        
        private class Cube
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public bool Active { get; set; }

            public Cube(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public override string ToString()
            {
                return Active ? "#" : ".";
            }

            public static bool operator !=(Cube cube1, Cube cube2)
            {
                return ! cube1.Equals(cube2);
            }

            public static bool operator ==(Cube cube1, Cube cube2)
            {
                return cube1.Equals(cube2);
            }
            
            public override bool Equals(object? obj)
            {
                var other = (Cube) obj;
                return other.X == X && other.Y == Y && other.Z == Z;
            }

            public override int GetHashCode()
            {
                return $"{X}{Y}{Z}".GetHashCode();
            }

            public int GetActiveNeighborsCount(World world)
            {
                var sum = 0;

                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        for (var z = -1; z <= 1; z++)
                        {
                            if (x == 0 && y == 0 && z == 0)
                            {
                                continue;
                            }

                            if (world.GetCubeAt(X+x, Y+y, Z+z).Active)
                            {
                                sum++;
                            }
                        }
                    }
                }
                
                return sum;
            }
        }
    }
}