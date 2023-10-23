using RestSharp;

namespace CastGroup.CommonServices
{
    public static class RestSharpRequests<T>
    {
        public static async Task<T?> Get(Uri fullUri)
        {
            RestClient client = new(fullUri.AbsoluteUri);
            return await client.GetAsync<T?>(new RestRequest());
        }

        public static async Task<T?> Post(Uri fullUri, object body)
        {
            RestClient client = new(fullUri.AbsoluteUri);
            RestRequest request = new();
            request.AddJsonBody(body);
            return await client.PostAsync<T?>(request);
        }

        public static async Task<T?> Put(Uri fullUri, object body)
        {
            RestClient client = new(fullUri.AbsoluteUri);
            RestRequest request = new();
            request.AddJsonBody(body);
            return await client.PutAsync<T?>(request);
        }

        public static async Task<T?> Delete(Uri fullUri)
        {
            RestClient client = new(fullUri.AbsoluteUri);
            return await client.DeleteAsync<T?>(new RestRequest());
        }
    }
}
