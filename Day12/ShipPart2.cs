using System;

namespace Day12
{
    public class ShipPart2
    {
        public int NorthPosition { get; set; }
        public int EastPosition { get; set; }
        public int ManhattanDistance => Math.Abs(NorthPosition) + Math.Abs(EastPosition);
        
        public ShipPart2()
        {
            NorthPosition = 0;
            EastPosition = 0;
        }
    }
}