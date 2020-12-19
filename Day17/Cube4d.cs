﻿namespace Day17
{
    internal class Cube4d : ICube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
        public bool Active { get; set; }

        public Cube4d(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public override string ToString()
        {
            return Active ? "#" : ".";
        }

        public static bool operator !=(Cube4d cube1, Cube4d cube2)
        {
            return ! cube1.Equals(cube2);
        }

        public static bool operator ==(Cube4d cube1, Cube4d cube2)
        {
            return cube1.Equals(cube2);
        }
        
        public override bool Equals(object? obj)
        {
            var other = (Cube4d) obj;
            return other.X == X && other.Y == Y && other.Z == Z && other.W == W;
        }

        public override int GetHashCode()
        {
            return $"{X}{Y}{Z}{W}".GetHashCode();
        }

        public int GetActiveNeighborsCount(World4d world)
        {
            var sum = 0;

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    for (var z = -1; z <= 1; z++)
                    {
                        for (var w = -1; w <= 1; w++)
                        {
                            if (x == 0 && y == 0 && z == 0 && w == 0)
                            {
                                continue;
                            }

                            if (world.GetCubeAt(X+x, Y+y, Z+z, W+w).Active)
                            {
                                sum++;
                            }
                        }
                    }
                }
            }
            
            return sum;
        }
    }
}