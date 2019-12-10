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

       
        public string Senha { get; set; }

		public int PessoaId { get; set; }
		public virtual Pessoa Pessoa { get; set; }

        public int EstacionamentoId { get; set; }
        public virtual Estacionamento Estacionamento { get; set; }

        public bool ativo { get; set; }
        public int CodigoDeAtivacaoId { get; set; }
        public virtual CodigoDeAtivacao CodigoDeAtivacao { get; set; }

    }
}