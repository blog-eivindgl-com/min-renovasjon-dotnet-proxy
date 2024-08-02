using MinRenovasjonProxy.Core.Model;

namespace MinRenovasjonProxy.Services
{
    public interface IHentekalenderService
    {
        Task<IEnumerable<Hentekalender>> GetHentekalenderAsync();
    }
}
