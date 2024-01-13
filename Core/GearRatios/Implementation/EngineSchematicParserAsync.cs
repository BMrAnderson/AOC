using Core.Extensions;
using System.Security;
using System.Text;

namespace Core.GearRatios.Implementation
{
    public class EngineSchematicParserAsync(IDocumentLinesReaderAsync documentsReader) : IEngineSchematicParserAsync
    {
        private const int MaxScanCount = 3;
        private static StringBuilder stringBuilder = new StringBuilder();

        private readonly IDocumentLinesReaderAsync _documentsReader = documentsReader;

        public async Task<IEnumerable<EngineSymbol>> ParseAsync(string engineSchematicDocument)
        {
            var lines = await _documentsReader.ReadAsync(engineSchematicDocument);
            var symbols = ParseSymbols(lines.ToArray());
            return symbols;
        }

        private IEnumerable<EngineSymbol> ParseSymbols(string[] schematicLines)
        {
            if (schematicLines.IsNullOrEmpty())
            {
                return Enumerable.Empty<EngineSymbol>();
            }
            var resultSet = new List<EngineSymbol>();

            var charLines = schematicLines.Select(line => line.Cast<char>().ToArray()).ToArray();

            for (var y = 0; y <= charLines.Length - 1; y++)
            {
                var line = charLines[y].ToArray();

                for (var x = 0; x <= line.Count() - 1; x++)
                {
                    if (!IsSymbol(line[x]))
                    {
                        continue;
                    }

                    int left = 0;
                    int right = 0;
                    int top = 0;
                    int topLeft = 0;
                    int topRight = 0;
                    int bottom = 0;
                    int bottomLeft = 0;
                    int bottomRight = 0;

                    //Can check top
                    if (y > 0)
                    {
                        if (!ParseVertical(x, charLines[y - 1], out top))
                        {
                            ParseLeft(x, charLines[y - 1], out topLeft);
                            ParseRight(x, charLines[y - 1], out topRight);
                        }
                    }
                    //Can check bottom
                    if (y < charLines.Length - 1)
                    {
                        if (!ParseVertical(x, charLines[y + 1], out bottom))
                        {
                            ParseLeft(x, charLines[y + 1], out bottomLeft);
                            ParseRight(x, charLines[y + 1], out bottomRight);
                        }
                    }
                    //Can check left
                    if (x > 0)
                    {
                        ParseLeft(x, line, out left);
                    }
                    //Can check right
                    if (x < line.Length - 1)
                    {
                        ParseRight(x, line, out right);
                    }

                    var symbol = new EngineSymbol(line[x]);
                    var edges = new int[] { top, topLeft, topRight, bottom, bottomLeft, bottomRight, left, right }.ToList();
                  
                    edges.ForEach(symbol.AddEdge);

                    if (symbol.Value <= 0)
                    {
                        continue;
                    }
                    resultSet.Add(symbol);
                }
            }
            return resultSet.ToArray();
        }

        private static bool ParseVertical(int index, char[] characters, out int value)
        {
            value = 0;

            for (int count = MaxScanCount; count >= 1; count--)
            {
                if (TryParse(index, count, characters, out int result))
                {
                    value = result;
                    return true;
                }
            }
            return false;
        }

        private static bool TryParse(int index, int count, char[] characters, out int result)
        {
            bool scanningOneDigit = count == 1;
            int startIndex = index - 1;

            if (scanningOneDigit)
            {
                startIndex = index;
            }

            //Parse middle 
            if (TryScan(startIndex, count, characters, out int a))
            {
                result = a; 
                return true;
            }

            if (scanningOneDigit)
            {
                result = 0;
                return false;
            }

            //Parse left
            startIndex = index - count + 1;

            if (TryScan(startIndex, count, characters, out int b))
            {
                result = b;
                return true;
            }

            //Parse right
            startIndex = index;

            if (TryScan(startIndex, count, characters, out int c))
            {
                result = c;
                return true;
            }

            result = 0;
            
            return false;
        }

        private static bool TryScan(int startIndex, int count, char[] chars, out int result)
        {
            result = 0;
            stringBuilder.Clear();

            try
            {
                int endIndex = startIndex + count;
                stringBuilder.Append(chars[startIndex..endIndex]);
            }
            catch (Exception)
            {
                return false;
            }

            var partString = stringBuilder.ToString();
            if (partString.Contains("."))
            {
                return false;
            }

            if (int.TryParse(stringBuilder.ToString(), out int val))
            {
                result = val;
                return true;
            }

            return false;
        }
      
        private static bool ParseLeft(int index, char[] chars, out int value)
        {
            value = 0;

            for (int count = MaxScanCount; count >= 1; count--)
            {
                if (TryScanLeft(index, count, chars, out int result))
                {
                    value = result;
                    return true;
                }
            }
            return false;
        }


        private static bool ParseRight(int index, char[] chars, out int value)
        {
            value = 0;

            for (int count = MaxScanCount; count >= 1; count--)
            {
                if (TryScanRight(index, count, chars, out int result))
                {
                    value = result;
                    return true;
                }
            }
            return false;
        }


        private static bool TryScanLeft(int index, int count, char[] chars, out int result)
        {
            int startIndex = index - count;

            if (count == 1)
            {
                startIndex = index - 1;
            }

            if (TryScan(startIndex, count, chars, out int val))
            {
                result = val;
                return true;
            }
            result = 0; 

            return false;
        }

        private static bool TryScanRight(int index, int count, char[] chars, out int result)
        {
            int startIndex = index + 1;

            if (TryScan(startIndex, count, chars, out int val))
            {
                result = val;
                return true;
            }
            result = 0;

            return false;
        }

        private static bool IsSymbol(char character)
        {
            if (character.IsWhiteSpace())
            {
                return false;
            }
            if (!char.TryParse(character.ToString(), out var c))
            {
                return false;
            }
            var result = !IsNumber(c) && c != '.';
        
            return result;   
        }

        private static bool IsNumber(char character)
        {
            var result = int.TryParse(character.ToString(), out var _);

            return result;
        }
    }
}

/*
 
//0 out

..999
..*..
..55.

* = index 2
999 = index 2
55 = index 2

//1 out

..999
.*...
..55.

999 = index 2
* = index 1
55 = index 2

.999..
..*...
.5....

999 = index 1
* = index 4
55 = index 1

...999..
.*......

 
 
 */