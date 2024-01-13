using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Scratchcards
{
    internal interface IScratchCard
    {
        void AddCopy(IScratchCard copy);
    }
}
