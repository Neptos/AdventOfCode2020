namespace Day12
{
    public class Instruction
    {
        public char Operation { get; set; }
        public int Amount { get; set; }

        public Instruction(char operation, int amount)
        {
            Operation = operation;
            Amount = amount;
        }
    }
}