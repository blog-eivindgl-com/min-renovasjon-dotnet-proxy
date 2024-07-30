using MinRenovasjonProxy.Core.Model.NorkartRenovasjon;

namespace MinRenovasjonProxy.Services
{
    public interface INorkartRenovasjonApiService
    {
        Task<TommekalenderResponse> GetTommekalenderAsync(TommekalenderRequest request);
        Task<FraksjonerResponse> GetFraksjonerAsync(FraksjonerRequest request);
    }
}
