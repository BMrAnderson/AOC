using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CubeConundrum.Implementation
{
    public class SubsetsLineParser(ISubsetFactory subsetFactory) : ISubsetsLineParser
    {
        private const string Red = "red";
        private const string Green = "green";
        private const string Blue = "blue";

        private readonly ISubsetFactory _subsetFactory = subsetFactory;

        public IEnumerable<Subset> Parse(string line)
        {
            var subsetsLine = GetSubsetsLine(line);
            var subsetLines = GetSubsetLines(subsetsLine);
            var subsets = ParseSubsets(subsetLines.ToArray());

            return subsets;
        }

        private string GetSubsetsLine(string line)
        {
            var trimmedLine = line.Replace(" ", "");
            var indexOfColon = trimmedLine.IndexOf(':');
            var result = trimmedLine.Remove(0, indexOfColon + 1);

            return result;
        }


        private IEnumerable<string> GetSubsetLines(string subsetsLine)
        {
            var result = subsetsLine.Split(';', StringSplitOptions.RemoveEmptyEntries);
            
            return result;
        }

        private IEnumerable<Subset> ParseSubsets(string[] subsetLines)
        {
            foreach (var line in subsetLines)
            {
                yield return ParseSubset(line);
            }
        }

        private Subset ParseSubset(string subsetLine)
        {
            var cubes = subsetLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
           
            var redCubes = ParseCube(Red, cubes);
            var greenCubes = ParseCube(Green, cubes);
            var blueCubes = ParseCube(Blue, cubes);

            return CreateSubset(redCubes, greenCubes, blueCubes);
        }

        private int ParseCube(string cubeColor, string[] cubes)
        {
            var specificCubeEntry = cubes.SingleOrDefault(cube => cube.Contains(cubeColor));
            if (string.IsNullOrEmpty(specificCubeEntry))
            {
                return 0;
            }
            var indexOfCubeColor = specificCubeEntry.IndexOf(cubeColor);
            var value = specificCubeEntry.Substring(0, indexOfCubeColor);
            var result = int.Parse(value);

            return result;
        } 

        private Subset CreateSubset(int redCubes, int greenCubes, int blueCubes)
        {
            var subset = _subsetFactory.Create(redCubes, greenCubes, blueCubes);

            return subset;
        }
    }
}
