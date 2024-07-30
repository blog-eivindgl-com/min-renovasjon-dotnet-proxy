namespace MinRenovasjonProxy.Core.Model.NorkartRenovasjon
{
    public class TommekalenderRequest
    {
        public string RenovasjonAppKey { get; set; }
        public string Kommunenr { get; set; }
        public string Server { get; set; }
        public string Gatenavn { get; set; }
        public string Gatekode { get; set; }
        public string Husnr { get; set; }
    }
}
