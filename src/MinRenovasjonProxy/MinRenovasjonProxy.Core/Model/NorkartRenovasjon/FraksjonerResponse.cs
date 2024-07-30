using System.Collections;

namespace MinRenovasjonProxy.Core.Model.NorkartRenovasjon
{
    public class FraksjonerResponse : IEnumerable<Fraksjon>
    {
        internal List<Fraksjon> _items;

        public FraksjonerResponse()
            : this(new List<Fraksjon>())
        { }

        public FraksjonerResponse(IEnumerable<Fraksjon> items)
        {
            _items = new List<Fraksjon>(items);
        }

        public IEnumerator<Fraksjon> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
