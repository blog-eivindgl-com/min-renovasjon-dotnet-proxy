namespace MinRenovasjonProxy.Core.Model.NorkartRenovasjon
{
    public class Fraksjon
    {
		public int Id { get; set; }
		public string Navn { get; set; }
		public string Ikon { get; set; }
		public int NorkartStandardFraksjonId { get; set; }
		public string NorkartStandardFraksjonIkon { get; set; }
    }
}
