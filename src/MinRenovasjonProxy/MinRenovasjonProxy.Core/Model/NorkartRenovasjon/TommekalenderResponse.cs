using System.Collections;

namespace MinRenovasjonProxy.Core.Model.NorkartRenovasjon
{
    public class TommekalenderResponse : IEnumerable<Tommekalenderelement>
    {
        internal List<Tommekalenderelement> _tommekalenderElementer;

        public TommekalenderResponse()
            : this(new List<Tommekalenderelement>())
        { }

        public TommekalenderResponse(IEnumerable<Tommekalenderelement> items)
        {
            _tommekalenderElementer = new List<Tommekalenderelement>(items);
        }

        public IEnumerator<Tommekalenderelement> GetEnumerator()
        {
            return _tommekalenderElementer.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tommekalenderElementer.GetEnumerator();
        }
    }
}
