using System.Collections;

namespace MinRenovasjonProxy.Core.Model.NorkartRenovasjon
{
    public class TommekalenderResponse : List<Tommekalenderelement>
    {

        public TommekalenderResponse()
            : this(new List<Tommekalenderelement>())
        { }

        public TommekalenderResponse(IEnumerable<Tommekalenderelement> items)
            : base(items)
        { }

    }
}
