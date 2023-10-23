using CastGroup.Api.Models;
using CastGroup.CommonServices;
using System.Text.RegularExpressions;

namespace CastGroup.Api.Services
{
    public partial class ViaCepService
    {
        private const string urlViaCep = "http://viacep.com.br/ws/";
        private const string tipoRetorno = "/json/";

        [GeneratedRegex("[^\\d]")]
        private static partial Regex OnlyNumberRegex();
        [GeneratedRegex("[0-9]{8}")]
        private static partial Regex CepRegex();

        public static async Task<ViaCepResponse?> GetViaCep(string cep)
        {
            var cepSemFormato = string.Join("", OnlyNumberRegex().Split(cep));
            if (CepValidate(cepSemFormato))
            {
                Uri fullUri = new(string.Concat(urlViaCep, cepSemFormato, tipoRetorno));
                return await RestSharpRequests<ViaCepResponse>.Get(fullUri);
            }

            return default;
        }

        private static bool CepValidate(string onlyNumberCep)
        {
            return CepRegex().IsMatch(onlyNumberCep);
        }
    }
}
