using MarsRovers.Domain.Models;
using MarsRovers.Domain.State;
using MarsRovers.Infrastructure;

ILogger logger = new ConsoleLogger();

// Assumption: Input files are '*.txt' files located in the App_Data folder 
const string inputFolder = @"..\..\..\App_Data";

if(Directory.Exists(inputFolder))
{
    var inputFiles = Directory.GetFiles(inputFolder, "*.txt").ToList();
    if(inputFiles.Any())
        inputFiles.ForEach(file => ProcessInputFile(file));
    else
        logger.Log($"Folder '{inputFolder}' does not contain input files.\n");
}
else
    logger.Log($"Input folder '{inputFolder}' does not exist.\n");

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
            // 1st line contains exploration area coordinates and expected to be present in an input file
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
