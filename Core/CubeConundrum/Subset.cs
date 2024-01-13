using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum
{
    public class Subset : IPossibilityCheck
    {
        //12 red cubes, 13 green cubes, and 14 blue cubes

        private const int MaxRedCubes = 12;
        private const int MaxGreenCubes = 13;
        private const int MaxBlueCubes = 14;

        public int GreenCubes { get; }
        public int BlueCubes { get; }
        public int RedCubes { get; }

        public Subset(int redCubes, int greenCubes, int blueCubes)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(redCubes, nameof(redCubes));
            ArgumentOutOfRangeException.ThrowIfNegative(greenCubes, nameof(greenCubes));
            ArgumentOutOfRangeException.ThrowIfNegative(blueCubes, nameof(blueCubes));

            GreenCubes = greenCubes; 
            BlueCubes  = blueCubes;  
            RedCubes   = redCubes;
        }

        public bool IsPossible()
        {
            return GreenCubes <= MaxGreenCubes
                && BlueCubes <= MaxBlueCubes
                && RedCubes <= MaxRedCubes;
        }
    }

}
