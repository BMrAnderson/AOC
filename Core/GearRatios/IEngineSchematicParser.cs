using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GearRatios
{
    public interface IEngineSchematicParserAsync
    {
        Task<IEnumerable<EngineSymbol>> ParseAsync(string engineSchematicDocument);
    }
}
