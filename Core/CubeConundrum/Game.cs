using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum
{
    public class Game : IIdentifier, IPossibilityCheck
    {
        private const int IdWithZeroValue = 0;

        public int Id { get; }

        public IEnumerable<Subset> Subsets { get; }

        public Game(int id, IEnumerable<Subset> subsets)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, IdWithZeroValue, nameof(id));

            Id = id;
            Subsets = subsets;
        }

        public int LeastPossibleGreenCubes
        {
            get
            {
                return Subsets.Select(set => set.GreenCubes).Max();
            }
        }

        public int LeastPossibleRedCubes
        {
            get
            {
                return Subsets.Select(set => set.RedCubes).Max();
            }
        }

        public int LeastPossibleBlueCubes
        {
            get
            {
                return Subsets.Select(set => set.BlueCubes).Max();
            }
        }

        public int PowerOfAllLeastPossibleCubes
        {
            get
            {
                return LeastPossibleBlueCubes * LeastPossibleGreenCubes * LeastPossibleRedCubes;
            }
        }

        public bool IsPossible()
        {
            return Subsets.All(subset => subset.IsPossible());
        }
    }
}
