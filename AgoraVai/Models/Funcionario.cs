using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Funcionario
    {
      // "^(?=.*\d)(?=.*[a-zA-Z])(?!.*[\W_\x7B-\xFF]).{4,15}$"

        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 12 caracteres")]
        [RegularExpression("^(?=.*)(?=.*[a-zA-Z])(?!.*[_\x7B-\xFF]).{8,12}$", ErrorMessage = "A Senha precisa conter pelo menos um numero, um caractere maiusculo e um minusculo.")]
        public string Senha { get; set; }

		public int PessoaId { get; set; }
		public virtual Pessoa Pessoa { get; set; }

        public int EstacionamentoId { get; set; }
        public virtual Estacionamento Estacionamento { get; set; }

    }
}