namespace MinRenovasjonProxy.Core.Model
{
    public class Hentekalender
    {
        public DateOnly Dato {  get; set; }
        public IEnumerable<string> Avfallstyper { get; set; }
    }
}
