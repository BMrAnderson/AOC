using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum.Implementation
{
    public class SubsetFactory : ISubsetFactory
    {
        public Subset Create(int redCubes, int greenCubes, int blueCubes)
        {
            ArgumentNullException.ThrowIfNull(redCubes, nameof(redCubes));
            ArgumentNullException.ThrowIfNull(greenCubes, nameof(greenCubes));
            ArgumentNullException.ThrowIfNull(blueCubes, nameof(blueCubes));

            var subset = new Subset(redCubes, greenCubes, blueCubes);

            return subset;
        }
    }
}
