namespace Day17
{
    internal class Operation
    {
        private ICube Cube { get; }

        public Operation(ICube cube)
        {
            Cube = cube;
        }

        public void Execute()
        {
            Cube.Active = !Cube.Active;
        }
    }
}