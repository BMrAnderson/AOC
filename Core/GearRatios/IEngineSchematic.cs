using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GearRatios
{
    public interface IEngineSchematic
    {
        Task<int> GetSumOfAllPartNumbersAsync(string engineSchematicDocument);
        Task<int> GetSumOfAllTwoPartNumbersAsync(string engineSchematicDocument);
    }
}
