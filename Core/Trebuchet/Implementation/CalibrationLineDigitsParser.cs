using Core.Extensions;

namespace Core.Trebuchet.Implementation
{
    internal class CalibrationLineDigitsParser : ICalibrationLineParser
    {
        private const int Zero = 0;
        private const int Negative = -1;

        private static IDictionary<string, int> _numberWordsKeyValues = new Dictionary<string, int>()
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5 },
            {"six", 6 },
            {"seven", 7 },
            {"eight", 8 },
            {"nine", 9 }
        };

        public int Parse(string line)
        {
            if (line.IsNullOrEmptyOrWhiteSpace())
            {
                return Zero;
            }
            return GetDigits(line);
        }

        private int GetDigits(string line)
        {
            int firstDigit = Negative;
            int lastDigit  = Negative;

            if (TryGetDigits(line, out firstDigit, out lastDigit))
            {
                var result = int.Parse($"{firstDigit}{lastDigit}");
                return result;
            }
            return Zero;
        }

        private bool TryGetDigits(string line, out int firstDigit, out int lastDigit)
        {
            if (!TryGetFirstDigit(line, out firstDigit))
            {
                firstDigit = 0;
            }
            if (!TryGetLastDigit(line, out lastDigit))
            {
                lastDigit = 0;
            }
            if (firstDigit == 0 && lastDigit > 0)
            {
                firstDigit = lastDigit;
            }
            if (lastDigit == 0 && firstDigit > 0)
            {
                lastDigit = firstDigit;
            }
            if (firstDigit == 0 && lastDigit == 0)
            {
                return false;
            }
            return true;
        }

        private bool TryGetFirstDigit(string line, out int firstDigit)
        {
            int result = -1;

            for (int index = 0; index < line.Length; index++)
            {
                if (result > 0)
                {
                    break;
                }

                for (int count = index; count < line.Length; count++)
                {                   
                    var character = line[count];
                    if (char.IsNumber(character))
                    {
                        result = int.Parse(character.ToString());
                        break;
                    }

                    var part = line.Substring(index, count + 1);
                    var key = part.WhereFirstMatchOrDefault(_numberWordsKeyValues.Keys) ?? string.Empty;
                    if (_numberWordsKeyValues.TryGetValue(key, out result))
                    {
                        break;
                    }
                }
            }
            firstDigit = result;
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        private bool TryGetLastDigit(string line, out int lastDigit)
        {
            int result = -1;

            for (int index = line.Length - 1; index > 0; index--)
            {
                if (result > 0)
                {
                    break;
                }
                var character = line[index];
                if (char.IsNumber(character))
                {
                    result = int.Parse(character.ToString());
                    break;
                }
                var part = line.Substring(index, line.Length - index);
                var key = part.WhereFirstMatchOrDefault(_numberWordsKeyValues.Keys);
                if (_numberWordsKeyValues.TryGetValue(key, out result))
                {
                    break;
                }
            }
            lastDigit = result;
            if (result < 1)
            {
                return false;
            }
            return true;

        }
    }
}
