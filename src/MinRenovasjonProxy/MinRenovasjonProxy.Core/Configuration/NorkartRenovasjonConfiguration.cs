namespace MinRenovasjonProxy.Core.Configuration
{
    public class NorkartRenovasjonConfiguration
    {
        public string AppKey { get; set; }  // See https://github.com/Danielhiversen/home_assistant_min_renovasjon
        public string Kommunenr { get; set; }
        public string Gatenavn { get; set; }
        public string Gatekode { get; set; }
        public string Husnr { get; set; }
    }
}
