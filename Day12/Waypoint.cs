namespace Day12
{
    public class Waypoint
    {
        private readonly ShipPart2 _ship;
        
        public int NorthPosition { get; set; }
        public int EastPosition { get; set; }

        public Waypoint(ShipPart2 ship)
        {
            _ship = ship;
            EastPosition = 10;
            NorthPosition = 1;
        }

        public void Rotate(char direction, int degrees)
        {
            var amountRight = direction == 'R' ? degrees : 360 - degrees;

            switch (amountRight)
            {
                case 90:
                    Rotate90();
                    break;
                case 180:
                    Rotate90();
                    Rotate90();
                    break;
                case 270:
                    Rotate90();
                    Rotate90();
                    Rotate90();
                    break;
            }
        }

        private void Rotate90()
        {
            var tempNorthPosition = NorthPosition;
            NorthPosition = EastPosition * -1;
            EastPosition = tempNorthPosition;
        }
    }
}