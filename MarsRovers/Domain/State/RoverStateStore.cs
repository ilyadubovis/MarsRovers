using MarsRovers.Domain.Models;
using Action = MarsRovers.Domain.Models.Action;

namespace MarsRovers.Domain.State
{
    /// <summary>
    /// RoverStateStore stores the current state of the rover
    /// </summary>
    public class RoverStateStore
    {
        public RoverState State { get; private set; }
        
        private readonly RoverStateReducer _reducer;

        public RoverStateStore(RoverState state, ExplorationArea explorationArea)
        {
            State = state;
            _reducer = new RoverStateReducer(explorationArea);
        }

        public void ApplyAction(Action action) =>
            State = _reducer.On(State, action);
    }
}
