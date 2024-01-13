using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum
{
    public interface ICubeConundrum
    {
        Task<int> GetSumOfIdsForPossbileGamesAsync(string documentPath);
        Task<int> GetSumOfThePowerOfSubsetsWithLeastPossibleCubesAsync(string documentPath);
    }
}
