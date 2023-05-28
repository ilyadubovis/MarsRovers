using MarsRovers.Domain.Models;
using System.Diagnostics;

namespace MarsRovers.Domain.State
{
    public struct RoverState
    {
        public readonly Position Position;
        public readonly Direction Direction;

        public RoverState(RoverState roverState) : this(roverState.Position, roverState.Direction)
        {
        }

        public RoverState(Position position, DirectionType directionType) : this(position, new Direction(directionType))
        {
        }

        public RoverState(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        public RoverState(string stateString)
        {
            if (string.IsNullOrWhiteSpace(stateString))
            {
                throw new ArgumentNullException(nameof(stateString));
            }
            var stateParams = stateString.Split(" ", StringSplitOptions.TrimEntries).ToList();
            if (stateParams.Count != 3 || stateParams.Any(p => string.IsNullOrEmpty(p)))
            {
                throw new ArgumentException("Rover state string is invalid.", nameof(stateString));
            }

            if (int.TryParse(stateParams[0], out int x) && int.TryParse(stateParams[1], out int y))
            {
                Position = new Position(x, y);
                try
                {
                    Direction = new Direction(stateParams[2].First());
                }
                catch(Exception e)
                {
                    throw new ArgumentException("Rover state string is invalid.", nameof(stateString), e);
                }
            }
            else 
            {
                throw new ArgumentException("Rover state string is invalid.", nameof(stateString));
            }
        }

        public override string ToString() =>
            $"{Position} {Direction.ToString().First()}";
    }
}
