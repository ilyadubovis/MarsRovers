namespace MarsRovers.Domain.Models
{
    public enum ActionType
    {
        Left,
        Right,
        Move
    }

    /// <summary>
    /// Action (command) that rover can execute
    /// </summary>
    public struct Action
    {
        public readonly ActionType Type;

        public Action(ActionType type) =>
            Type = type;

        public Action(char abbreviation) =>
            Type = Char.ToUpperInvariant(abbreviation) switch {
                'L' => ActionType.Left,
                'R' => ActionType.Right,
                'M' => ActionType.Move,
                _ => throw new ArgumentException("Unsupported action type.", nameof(abbreviation))
            };

        public override string ToString() =>
            $"{Type}";
    }
}
