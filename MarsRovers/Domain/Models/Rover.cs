using MarsRovers.Domain.State;
using MarsRovers.Infrastructure;

namespace MarsRovers.Domain.Models
{
    /// <summary>
    /// Rover instance
    /// Two parameters are required to initialize a rover: exploration area and an initial state
    /// </summary>
    public class Rover
    {
        private static int NextId = 1;
        
        private readonly int _id;
        private readonly ExplorationArea _explorationArea;
        private RoverStateStore _stateStore;
        private readonly ILogger? _logger;

        public static Rover CreateRover(ExplorationArea explorationArea, RoverState state, ILogger? logger = null) =>
            new(Rover.NextId++, explorationArea, state, logger);

        private Rover(int id, ExplorationArea explorationArea, RoverState state, ILogger? logger = null)
        {
            _id = id;
            _explorationArea = explorationArea;
            _logger = logger;
            if (!_explorationArea.ContainsPosition(state.Position))
            {
                throw new ArgumentOutOfRangeException(nameof(state), "Position is outside of the exploration area.");
            }
            _stateStore = new RoverStateStore(state, _explorationArea);
           _logger?.Log($"Rover {_id}: State: {state}");
        }

        public void ExecuteAction(Action action) 
        { 
            _stateStore.ApplyAction(action);
            _logger?.Log($"Rover {_id}: Action: {action} => New state: {State}");
        }

        public void ExecuteActionChain(ActionChain actionChain) =>
            actionChain.Actions.ForEach(action => ExecuteAction(action));

        public RoverState State => 
            _stateStore.State;
    }
}
