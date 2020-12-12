using System;

namespace Day12
{
    public class Ship
    {
        private int _facingDegrees;
        public int NorthPosition { get; set; }
        public int EastPosition { get; set; }

        public int FacingDegrees
        {
            get => _facingDegrees;
            set
            {
                _facingDegrees = value % 360;
                if (_facingDegrees < 0)
                {
                    _facingDegrees += 360;
                }
            }
        }

        public char ForwardDirection
        {
            get
            {
                return FacingDegrees switch
                {
                    0 => 'N',
                    90 => 'E',
                    180 => 'S',
                    270 => 'W',
                    _ => throw new Exception($"Unknown direction {FacingDegrees}")
                };
            }
        }

        public int ManhattanDistance => Math.Abs(NorthPosition) + Math.Abs(EastPosition);

        public Ship()
        {
            FacingDegrees = 90;
            NorthPosition = 0;
            EastPosition = 0;
        }

        public void Execute(Instruction instruction)
        {
            switch (instruction.Operation)
            {
                case 'N':
                    NorthPosition += instruction.Amount;
                    break;
                case 'E':
                    EastPosition += instruction.Amount;
                    break;
                case 'S':
                    NorthPosition -= instruction.Amount;
                    break;
                case 'W':
                    EastPosition -= instruction.Amount;
                    break;
                case 'L':
                    FacingDegrees -= instruction.Amount;
                    break;
                case 'R':
                    FacingDegrees += instruction.Amount;
                    break;
                case 'F':
                    Execute(new Instruction(ForwardDirection, instruction.Amount));
                    break;
            }
        }
    }
}