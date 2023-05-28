using MarsRovers.Domain.Models;
using Action = MarsRovers.Domain.Models.Action;

namespace MarsRovers.Domain.State
{
    /// <summary>
    /// RoverStateReducer is intended to produce a new rover state by applying a requested action to the current state
    /// </summary>
    class RoverStateReducer
    {
        private readonly ExplorationArea _explorationArea;

        public RoverStateReducer(ExplorationArea explorationArea) =>
            _explorationArea = explorationArea;

        /// <summary>
        /// Produce a new rover state by applying a requested action to the current state
        /// </summary>
        /// <param name="roverState"></param>
        /// <param name="action"></param>
        /// <returns>new state</returns>
        public RoverState On(RoverState roverState, Action action) =>
            action.Type switch
            {
                ActionType.Left or ActionType.Right => OnTurn(roverState, action),
                ActionType.Move => OnMove(roverState),
                _ => roverState
            };

        private RoverState OnTurn(RoverState roverState, Action action) =>
            new(roverState.Position, GetDirectionTypeOn(roverState.Direction.Type, action));
        
        private RoverState OnMove(RoverState roverState) =>
            new(GetPositionOnMove(roverState), roverState.Direction);

        private Position GetPositionOnMove(RoverState roverState)
        {
            var x = roverState.Position.X;
            var y = roverState.Position.Y;
            var position = roverState.Direction.Type switch
            {
                DirectionType.North => new Position(x, ++y),
                DirectionType.East  => new Position(++x, y),
                DirectionType.South => new Position(x, --y),
                DirectionType.West  => new Position(--x, y),
                _ => roverState.Position
            };

            // if a requested position is not within the exploration area => return the current position (i.e., a requested move will not happen)
            return _explorationArea.ContainsPosition(position) ? position : roverState.Position;
        }

        private DirectionType GetDirectionTypeOn(DirectionType currentDirectionType, Action action) =>
            action.Type switch
            {
                ActionType.Left => currentDirectionType switch
                {
                    DirectionType.North => DirectionType.West,
                    DirectionType.East  => DirectionType.North,
                    DirectionType.South => DirectionType.East,
                    DirectionType.West  => DirectionType.South,
                    _ => currentDirectionType

                },
                ActionType.Right => currentDirectionType switch
                {
                    DirectionType.North => DirectionType.East,
                    DirectionType.East  => DirectionType.South,
                    DirectionType.South => DirectionType.West,
                    DirectionType.West  => DirectionType.North,
                    _ => currentDirectionType
                },
                _ => currentDirectionType
            };
    }
}
