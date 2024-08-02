namespace MinRenovasjonProxy.Core.Model.NorkartRenovasjon
{
    public class Tommekalenderelement
    {
        public int FraksjonId { get; set; }
        public IEnumerable<DateTime> Tommedatoer { get; set; }
    }
}
