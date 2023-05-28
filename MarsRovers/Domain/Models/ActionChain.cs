namespace MarsRovers.Domain.Models
{
    /// <summary>
    /// List of actions that rover should execute
    /// </summary>
    public class ActionChain
    {
        public readonly List<Action> Actions; 

        public ActionChain(List<Action> actions)
        {
            Actions = actions;
        }

        public ActionChain(string actionAbbreviations)
        {
            if (string.IsNullOrWhiteSpace(actionAbbreviations))
            {
                throw new ArgumentNullException(nameof(actionAbbreviations));
            }
            
            Actions = new List<Action>();
            foreach (var abbreviation in actionAbbreviations.ToCharArray())
            {
                try
                {
                    Actions.Add(new Action(abbreviation));
                }
                catch (ArgumentException) 
                {
                    continue; // ignore unsupported action
                }
            }
        }
    }
}
