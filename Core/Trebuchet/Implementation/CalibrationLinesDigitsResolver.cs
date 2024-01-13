using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Trebuchet.Implementation
{
    public class CalibrationLinesDigitsResolver(ICalibrationLineParser lineParser) : ICalibrationLinesDigitsResolver
    {
        public int Resolve(IEnumerable<string> lines)
        {
            return CalculateTotal(lines);
        }

        private int CalculateTotal(IEnumerable<string> lines)
        {
            var values = lines.Select(lineParser.Parse);

            var result = values.Sum();

            return result;
        }
    }
}
