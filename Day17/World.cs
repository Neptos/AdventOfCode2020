using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
{
    internal class World
    {
        public Dictionary<int, Dictionary<int, Dictionary<int, Cube>>> Cubes { get; }

        public World()
        {
            Cubes = new Dictionary<int, Dictionary<int, Dictionary<int, Cube>>>();
        }
        
        public void ExpandWorld()
        {
            var xMax = Cubes.Keys.Max();
            var xMin = Cubes.Keys.Min();
            var yMax = Cubes[0].Keys.Max();
            var yMin = Cubes[0].Keys.Min();
            var zMax = Cubes[0][0].Keys.Max();
            var zMin = Cubes[0][0].Keys.Min();
            for (var x = xMin-1; x <= xMax+1; x++)
            {
                for (var y = yMin-1; y <= yMax+1; y++)
                {
                    for (var z = zMin-1; z <= zMax+1; z++)
                    {
                        GetCubeAt(x, y, z);
                        GetCubeAt(x, y, z);
                    }
                }
            }
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
}