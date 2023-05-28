using MarsRovers.Domain.Models;
using MarsRovers.Domain.State;
using MarsRovers.Infrastructure;

ILogger logger = new ConsoleLogger();

Directory.GetFiles(@"..\..\..\App_Data").ToList()
    .ForEach(file => ProcessInputFile(file));

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

void ProcessInputFile(string inputFile)
{
    logger.Log($"* Processing input from {new FileInfo(inputFile).Name}...");

    var inputLines = new Queue<string>(File.ReadAllLines(inputFile));
    if (inputLines.Any())
    {
        try
        {
            var explorationArea = new ExplorationArea(inputLines.Dequeue());
            while (true)
            {
                if (inputLines.TryDequeue(out string? initialStateSting) && initialStateSting != default)
                {
                    var initialState = new RoverState(initialStateSting);
                    var rover = Rover.CreateRover(explorationArea, initialState, logger);
                    if (inputLines.TryDequeue(out string? actionChainString) && actionChainString != default)
                    {
                        rover.ExecuteActionChain(new ActionChain(actionChainString));
                        logger.Log($"{rover.State}\n");
                    }
                    else
                        break;
                }
                else
                    break;
            }
        }
        catch (Exception e)
        {
            logger.Log(e);
        }
    }
}
