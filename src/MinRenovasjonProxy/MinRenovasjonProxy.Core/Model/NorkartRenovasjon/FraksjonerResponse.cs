namespace MinRenovasjonProxy.Core.Model.NorkartRenovasjon
{
    public class FraksjonerResponse : List<Fraksjon>
    {
        public FraksjonerResponse()
            : this(new List<Fraksjon>())
        { }

        public FraksjonerResponse(IEnumerable<Fraksjon> items)
            : base(items)
        { }
    }
}
