namespace Core.Scratchcards
{
    public class Card(int id, IEnumerable<int> winningNumbers, IEnumerable<int> actualNumbers)
    {
        public int Id { get; } = id;
        public IEnumerable<int> WinningNumbers { get; } = winningNumbers;
        public IEnumerable<int> ActualNumbers { get; } = actualNumbers;
        
        public int TotalMatches => ActualNumbers.Count(WinningNumbers.Contains);

        public bool HasMatch => TotalMatches > 0;

        public int GetTotalPoints()
        {
            const int doubled = 2;
            
            if (!HasMatch)
            {
                return 0;
            }

            int result = Enumerable.Range(1, TotalMatches).Aggregate((matches, _) => matches * doubled);  
            return result;
        }
    }
}
