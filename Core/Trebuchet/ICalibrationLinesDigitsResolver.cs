namespace Core.Trebuchet
{
    public interface ICalibrationLinesDigitsResolver
    {
        public int Resolve(IEnumerable<string> lines);
    }
}
