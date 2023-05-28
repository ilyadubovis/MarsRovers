namespace MarsRovers.Infrastructure
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception exception, bool logStackTrace = false);
    }
}
