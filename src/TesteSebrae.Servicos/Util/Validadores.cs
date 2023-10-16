using System.Text.RegularExpressions;

namespace TesteSebrae.Servicos.Util
{
    public static class Validadores
    {
        public static bool ValidaCep(string cep)
        {
            if (cep.Length == 8)
            {
                cep = string.Concat(cep.AsSpan(0, 5), "-", cep.AsSpan(5, 3));
            }
            return Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }
    }
}
