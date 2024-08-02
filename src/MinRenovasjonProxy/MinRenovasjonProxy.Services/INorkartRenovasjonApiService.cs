using MinRenovasjonProxy.Core.Model.NorkartRenovasjon;

namespace MinRenovasjonProxy.Services
{
    public interface INorkartRenovasjonApiService
    {
        Task<TommekalenderResponse?> GetTommekalenderAsync();
        Task<FraksjonerResponse?> GetFraksjonerAsync();
    }
}
