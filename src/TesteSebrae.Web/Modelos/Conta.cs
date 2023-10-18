using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TesteSebrae.Web.Modelos
{
    public class Conta
    {
        [Key]
        [DisplayName("Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Digite o nome")]
        [MinLength(3)]
        [DisplayName("Nome completo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite a descrição")]
        [MinLength(3)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; } = string.Empty;


        public static implicit operator Conta(Dominio.Conta conta)
        {
            return new Conta
            {
                Id = conta.Id,
                Descricao = conta.Descricao,
                Nome = conta.Nome
            };
        }

        public static implicit operator Dominio.Conta(Conta conta)
        {
            return new Dominio.Conta
            {
                Id = conta.Id,
                Descricao = conta.Descricao,
                Nome = conta.Nome
            };
        }
    }
}
