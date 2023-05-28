namespace MarsRovers.Domain.Models
{
    /// <summary>
    /// Rectangular area from _minPosition to _maxPosition, which rover can move within
    /// Could be initialized with 2 positions or just the _maxPosition, assuming the _minPosition is (0,0)
    /// </summary>
    public struct ExplorationArea
    {
        private readonly Position _minPosition;
        private readonly Position _maxPosition;

        public ExplorationArea(Position maxPosition) : this (new Position(), maxPosition)
        {
        }

        public ExplorationArea(Position minPosition, Position maxPosition)
        {
            if(minPosition.X >= maxPosition.X || minPosition.Y >= maxPosition.Y)
            {
                throw new ArgumentException("Exploration area dimensions are invalid.", nameof(maxPosition));
            }
            _minPosition = minPosition;
            _maxPosition = maxPosition;
        }

        public ExplorationArea(string dimensionString)
        {
            if (string.IsNullOrWhiteSpace(dimensionString))
            {
                throw new ArgumentNullException(nameof(dimensionString));
            }
            var dimensions = dimensionString.Split(" ", StringSplitOptions.TrimEntries).ToList();
            if (dimensions.Count == 2) // only position of the upper right corner is provider, the lover left corner is (0,0)
            {
                _minPosition = new Position();
                if (int.TryParse(dimensions[0], out int x) && int.TryParse(dimensions[1], out int y))
                {
                    if (_minPosition.X >= x || _minPosition.Y >= y)
                    {
                        throw new ArgumentException("Exploration area coordinates are invalid.", nameof(dimensionString));
                    }
                    _maxPosition = new Position(x, y);
                }
                else
                {
                    throw new ArgumentException("Dimension string is invalid.", nameof(dimensionString));
                }
            }
            else if (dimensions.Count == 4) // positions of the lover left and the upper right corners are provided
            {
                if (int.TryParse(dimensions[0], out int x0) && int.TryParse(dimensions[1], out int y0) &&
                    int.TryParse(dimensions[2], out int x1) && int.TryParse(dimensions[3], out int y1))
                {
                    if (x0 >= x1 || y0 >= y1)
                    {
                        throw new ArgumentException("Exploration area coordinates are invalid.", nameof(dimensionString));
                    }
                    _minPosition = new Position(x0, y0);
                    _maxPosition = new Position(x1, y1);
                }
                else
                {
                    throw new ArgumentException("Dimension string is invalid.", nameof(dimensionString));
                }
            }
            else
            {
                throw new ArgumentException("Dimension string is invalid.", nameof(dimensionString));
            }
        }

        public bool ContainsPosition(Position position) =>
            position.X >= _minPosition.X && position.X <= _maxPosition.X && position.Y >= _minPosition.Y && position.Y <= _maxPosition.Y;
    }
}
