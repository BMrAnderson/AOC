using System.Diagnostics;

namespace Core.Scratchcards.Implementation
{
    public class ScratchCards(ICardLineParser lineParser, IDocumentLinesReaderAsync documentLinesReader) : IScratchCards
    {
        private const int TotalCards = 201;

        private readonly ICardLineParser _lineParser = lineParser;
        private readonly IDocumentLinesReaderAsync _documentLinesReader = documentLinesReader;

        private readonly IDictionary<int, int[]> _winningCardCopies = new Dictionary<int, int[]>(TotalCards);
        private readonly IDictionary<int, int> _keyCardPoints = new Dictionary<int, int>(TotalCards);

        public async Task<int> GetTotalPoints(string scratchCardDocument)
        {
            var cards = await GetCards(scratchCardDocument);
            var result = cards.Sum(card => card.GetTotalPoints());
            return result;
        }

        public async Task<int> GetTotalWinningScratchCards(string scratchCardDocument)
        {
            var cards = await GetCards(scratchCardDocument);

            AllocateCopiesForScratchCards(cards);
            AllocatePointsForScratchCards();

            return CalculateTotalPoints();
        }
        
        private void AllocateCopiesForScratchCards(Card[] allCards)
        {
            for (var i = 0; i < allCards.Length; i++)
            {
                var card = allCards[i];
                _winningCardCopies.Add(card.Id, Array.Empty<int>());

                if (!card.HasMatch) 
                {
                    continue;
                }
                var copies = allCards
                    .Skip(i + 1)
                    .Take(card.TotalMatches)
                    .Select(card => card.Id)
                    .ToArray();
               
                _winningCardCopies[card.Id] = copies;
            }
        }

        private void AllocatePointsForScratchCards()
        {
            for (int y = _winningCardCopies.Keys.Count - 1; y >= 0; y--)
            {
                var card = _winningCardCopies.ElementAt(y);
                var totalCopies = card.Value.Length;

                for (int x = 0; x <= card.Value.Length - 1; x++)
                {
                    var copy = card.Value[x];
                    if (_keyCardPoints.TryGetValue(copy, out int copyPoints))
                    {
                        totalCopies += copyPoints;
                    }         
                }
                _keyCardPoints.Add(card.Key, totalCopies);
            }
        }

        private int CalculateTotalPoints()
        {
            int totalOriginalCards = _keyCardPoints.Keys.Count;
            int totalCopyPoints = _keyCardPoints.Sum(card => card.Value);
            int result = totalOriginalCards + totalCopyPoints;

            return result;
        }

        private async Task<Card[]> GetCards(string scratchCardDocument)
        {
            var lines = await _documentLinesReader.ReadAsync(scratchCardDocument);
            var cards = Parse(lines.ToArray());
            return cards;
        }

        private Card[] Parse(string[] lines)
        {
            var result = lines.Select(_lineParser.Parse).ToArray();
            return result;
        }
    }
}

/*
 
 

1 = 2,3,4,5       +4 +6 +3 +1 = +14     
    2 = 3,4
        3 = 4,5
            4 = 5
        4 = 5

    3 = 4,5
        4 = 5

    4 = 5
        
2 = 3,4         +2 +3 +1 = +6
    3 = 4,5
        4 = 5

    4 = 5                                              

3 = 4,5         +2 +1 = +3

    4 = 5 (1)

4 = 5           +1

5               

6               
      
= 30

 */