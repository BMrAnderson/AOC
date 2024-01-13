namespace Core.CubeConundrum.Implementation
{
    public class GameLineParser(IGameFactory gameFactory, ISubsetsLineParser subsetsLineParser) : IGameLineParser
    {
        private readonly IGameFactory _gameFactory = gameFactory;
        private readonly ISubsetsLineParser _subsetsLineParser = subsetsLineParser;

        public Game Parse(string line)
        {
            ArgumentException.ThrowIfNullOrEmpty(line);
            ArgumentException.ThrowIfNullOrWhiteSpace(line);

            var gameId = ParseGameId(line);
            var subsets = ParseSubsets(line);

            return GetGame(gameId, subsets);
        }

        private Game GetGame(int gameId, IEnumerable<Subset> subsets) 
        { 
            var game = _gameFactory.Create(gameId, subsets);
          
            return game;
        }

        private int ParseGameId(string line)
        {
            var idWithColonLeft = line.Remove(0, 5);
            var indexOfColon = idWithColonLeft.IndexOf(':');
            var idString = idWithColonLeft.Substring(0, indexOfColon);
            var id = int.Parse(idString);

            return id;
        }

        private IEnumerable<Subset> ParseSubsets(string line)
        {
            var subsets = _subsetsLineParser.Parse(line);

            return subsets;
        }
    }
}
