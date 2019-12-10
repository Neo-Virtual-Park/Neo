using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class TrocarSenha
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Codigo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 12 caracteres")]
        [RegularExpression("^(?=.*)(?=.*[a-zA-Z])(?!.*[_\x7B-\xFF]).{8,12}$", ErrorMessage = "A Senha precisa conter pelo menos um numero, um caractere maiusculo e um minusculo.")]
        public string Senha { get; set; }

        [Required]
        [Compare(nameof(Senha), ErrorMessage = "Senhas Diferentes")]
        public string ConfirmarSenha { get; set; }
    }
}