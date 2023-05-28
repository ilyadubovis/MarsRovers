namespace MarsRovers.Domain.Models
{
    public enum DirectionType
    {
        North,
        East,
        South,
        West,
    }

    /// <summary>
    /// Direction, which rover could point to
    /// </summary>
    public struct Direction
    {
        public readonly DirectionType Type;

        public Direction(DirectionType type) =>
            Type = type;

        public Direction(char abbreviation) =>
            Type = Char.ToUpperInvariant(abbreviation) switch
            {
                'N' => DirectionType.North,
                'E' => DirectionType.East,
                'S' => DirectionType.South,
                'W' => DirectionType.West,
                _ => throw new ArgumentException("Unsupported direction.", nameof(abbreviation))
            };

        public override string ToString() =>
            $"{Type.ToString().First()}";
    }
}
