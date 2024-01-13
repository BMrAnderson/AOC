using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum
{
    public interface IGameFactory
    {
        Game Create(int id, IEnumerable<Subset> subsets);
    }
}
