namespace MarsRovers.Infrastructure
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) =>
            Console.WriteLine(message);

        public void Log(Exception exception, bool logStackTrace = false)
        {
            Console.WriteLine(exception.Message);
            if(logStackTrace)
            {
                Console.WriteLine(exception.StackTrace);    
            }
        }
    }
}
