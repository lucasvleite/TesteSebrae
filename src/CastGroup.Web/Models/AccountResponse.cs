﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CastGroup.Web.Models
{
    public class AccountResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3)]
        [MaxLength(200)]
        [DisplayName("Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(5)]
        [MaxLength(500)]
        [DisplayName("Descrição")]
        public string Description { get; set; } = string.Empty;
    }
}