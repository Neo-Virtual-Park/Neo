using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Nome { get; set; }

        [Required]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string cpf { get; set; }
    }
}