namespace MarsRovers.Domain.Models
{
    /// <summary>
    /// Position on the explaration area (X, Y)
    /// </summary>
    public struct Position
    {
        public readonly int X;
        public readonly int Y;

        public Position() : this (0, 0)
        {
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => 
            $"{X} {Y}";
    }
}
