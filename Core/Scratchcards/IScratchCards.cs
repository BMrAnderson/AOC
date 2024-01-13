using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Scratchcards
{
    public interface IScratchCards
    {
        public Task<int> GetTotalPoints(string scratchCardDocument);
        public Task<int> GetTotalWinningScratchCards(string scratchCardDocument);
    }
}
