using Core.Extensions;

namespace Core.CubeConundrum.Implementation
{
    public class CubeConundrum(IDocumentLinesReaderAsync documentsReader, IGameLineParser gameLineParser) : ICubeConundrum
    {
        private readonly IDocumentLinesReaderAsync _documentsReader = documentsReader;
        private readonly IGameLineParser _gameLineParser = gameLineParser;

        public async Task<int> GetSumOfIdsForPossbileGamesAsync(string documentPath)
        {
            var possibleGames = await GetPossibleGamesAsync(documentPath);
            var result = SumTotalPossibleGameIds(possibleGames.ToArray());
            
            return result;
        }

        public async Task<int> GetSumOfThePowerOfSubsetsWithLeastPossibleCubesAsync(string documentPath)
        {
            var games = await ParseGamesAsync(documentPath);
            var result = games.Sum(game => game.PowerOfAllLeastPossibleCubes); 

            return result;
        }

        private async Task<string[]> GetGameLinesAsync(string documentPath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(documentPath, nameof(documentPath));
            ArgumentException.ThrowIfNullOrEmpty(documentPath, nameof(documentPath));

            var documentLines = await _documentsReader.ReadAsync(documentPath);

            return documentLines.ToArray();
        }

        private async Task<IEnumerable<Game>> ParseGamesAsync(string documentPath)
        {
            var documentLines = await GetGameLinesAsync(documentPath);
            if (documentLines.IsNullOrEmpty())
            {
                return Enumerable.Empty<Game>();
            }
            var games = documentLines.Select(_gameLineParser.Parse).ToArray();
            return games;
        }

        private async Task<IEnumerable<Game>> GetPossibleGamesAsync(string documentPath)
        {
            var games = await ParseGamesAsync(documentPath);
            var result = games.Where(game => game.IsPossible()).ToArray();

            return result;
        }

        private int SumTotalPossibleGameIds(IIdentifier[] games)
        {
            var result = games.Sum(game => game.Id);  

            return result;
        }
    }
}
