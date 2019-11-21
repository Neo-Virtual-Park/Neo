using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Sangria
    {
        [Key]
        public int Id { get; set; }

        public string descricao { get; set; }

        public float valor { get; set; }

        public int confirmar { get; set; }

        public int FuncionarioId { get; set; }

        public DateTime horadasangria { get; set; }

        public virtual Funcionario Funcionario { get; set; }

    }
}