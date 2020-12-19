namespace Day17
{
    internal class Cube : ICube
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