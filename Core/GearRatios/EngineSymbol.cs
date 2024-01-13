using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GearRatios;

public struct EngineSymbol
{
    private IList<int> _edges = new List<int>();

    public EngineSymbol(char gear)
    {
        Gear = gear;
    }

    public char Gear { get; }

    public int Value => _edges.Sum();

    public IEnumerable<int> Edges => _edges;

    public IEnumerable<int> NonEmptyEdges
    {
        get
        {
            return _edges.Where(x => x != 0);
        }
    }

    public void AddEdge(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        _edges.Add(value);
    }

    public bool IsGearRatio()
    {
        return Gear == '*' && NonEmptyEdges.Count() == 2;
    }

    public int TryGetGearRatioValue()
    {
        if (IsGearRatio())
        {
            return NonEmptyEdges.First() * NonEmptyEdges.Last();
        }
        return 0;
    }
}
