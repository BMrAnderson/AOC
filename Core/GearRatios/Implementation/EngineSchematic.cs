using System.Diagnostics;

namespace Core.GearRatios.Implementation;

public class EngineSchematic(IEngineSchematicParserAsync engineParser) : IEngineSchematic
{
    private readonly IEngineSchematicParserAsync _engineParser = engineParser;

    public async Task<int> GetSumOfAllPartNumbersAsync(string engineSchematicDocument)
    {
        var symbols = await _engineParser.ParseAsync(engineSchematicDocument);
        var result = symbols.Sum(symbol => symbol.Value);
        return result;
    }

    public async Task<int> GetSumOfAllTwoPartNumbersAsync(string engineSchematicDocument)
    {
        var symbols = await _engineParser.ParseAsync(engineSchematicDocument);
        var twoPartSymbols = symbols.Where(symbol => symbol.IsGearRatio()).ToArray();

        for (int i = 0; i < twoPartSymbols.Length; i++)
        {
            EngineSymbol symbol = twoPartSymbols[i];
            Debug.WriteLine("");
            Debug.WriteLine($"({i}):");
            Debug.WriteLine("////////////");
            foreach (var item in symbol.NonEmptyEdges)
            {
                Debug.WriteLine($"- {item}");
            }
            Debug.WriteLine("////////////"); 
        }
        var result = twoPartSymbols.Sum(symbol => symbol.TryGetGearRatioValue());
        return result;
    }
}
