using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
{
    internal class World4d
    {
        public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Cube4d>>>> Cubes { get; }

        public World4d()
        {
            Cubes = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Cube4d>>>>();
        }
        
        public void ExpandWorld()
        {
            var xMax = Cubes.Keys.Max();
            var xMin = Cubes.Keys.Min();
            var yMax = Cubes[0].Keys.Max();
            var yMin = Cubes[0].Keys.Min();
            var zMax = Cubes[0][0].Keys.Max();
            var zMin = Cubes[0][0].Keys.Min();
            var wMax = Cubes[0][0][0].Keys.Max();
            var wMin = Cubes[0][0][0].Keys.Min();
            for (var x = xMin-1; x <= xMax+1; x++)
            {
                for (var y = yMin-1; y <= yMax+1; y++)
                {
                    for (var z = zMin-1; z <= zMax+1; z++)
                    {
                        for (var w = wMin-1; w <= wMax+1; w++)
                        {
                            GetCubeAt(x, y, z, w);
                            GetCubeAt(x, y, z, w);
                        }
                    }
                }
            }
        }

        public Cube4d GetCubeAt(int x, int y, int z, int w)
        {
            if (Cubes.ContainsKey(x))
            {
                if (Cubes[x].ContainsKey(y))
                {
                    if (Cubes[x][y].ContainsKey(z))
                    {
                        if (!Cubes[x][y][z].ContainsKey(w))
                        {
                            Cubes[x][y][z][w] = new Cube4d(x, y, z, w);
                        }
                    }
                    else
                    {
                        Cubes[x][y][z] = new Dictionary<int, Cube4d>();
                        Cubes[x][y][z][w] = new Cube4d(x, y, z, w);
                    }
                }
                else
                {
                    Cubes[x][y] = new Dictionary<int, Dictionary<int, Cube4d>>();
                    Cubes[x][y][z] = new Dictionary<int, Cube4d>();
                    Cubes[x][y][z][w] = new Cube4d(x, y, z, w);
                }
            }
            else
            {
                Cubes[x] = new Dictionary<int, Dictionary<int, Dictionary<int, Cube4d>>>();
                Cubes[x][y] = new Dictionary<int, Dictionary<int, Cube4d>>();
                Cubes[x][y][z] = new Dictionary<int, Cube4d>();
                Cubes[x][y][z][w] = new Cube4d(x, y, z, w);
            }

            return Cubes[x][y][z][w];
        }
    }
}