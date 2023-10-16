namespace TesteSebrae.Servicos.Interfaces
{
    public interface IRequisicaoViaCep<T>
    {
        Task<T?> ChamaViaCep(string cep);
    }
}
