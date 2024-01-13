using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum
{
    public interface ISubsetFactory
    {
        Subset Create(int redCubes, int greenCubes, int blueCubes);
    }
}
