using RestSharp;
using System.Text.RegularExpressions;
using TesteSebrae.Servicos.Interfaces;

namespace TesteSebrae.Servicos
{
    public class RequisicaoViaCep<T> : IRequisicaoViaCep<T> where T : class
    {
        private const string urlViaCep = "http://viacep.com.br/ws/";
        private const string tipoRetorno = "/json/";

        public async Task<T?> ChamaViaCep(string cep)
        {
            var cepSemFormato = string.Join("", Regex.Split(cep, @"[^\d]"));
            string requisicaoCep = string.Concat(cepSemFormato, tipoRetorno);

            var opcoes = new RestClientOptions(urlViaCep);
            var cliente = new RestClient(opcoes);
            var request = new RestRequest(requisicaoCep);
            var response = await cliente.GetAsync<T>(request);

            return response;
        }
    }
}