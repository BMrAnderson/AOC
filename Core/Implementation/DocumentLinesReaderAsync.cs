using System.Text;

namespace Core.Implementation
{
    public class DocumentLinesReaderAsync : IDocumentLinesReaderAsync
    {
        public async Task<IEnumerable<string>> ReadAsync(string filepath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filepath);
            ArgumentException.ThrowIfNullOrEmpty(filepath);
            try
            {
                var lines = await File.ReadAllLinesAsync(filepath, Encoding.Default);
                if (lines == null)
                {
                    return Enumerable.Empty<string>();  
                }
                return lines;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
