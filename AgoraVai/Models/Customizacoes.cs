using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Customizacoes
    {
        [Key]
        public int Id { get; set; }

        public int MinutosDeTolerancia { get; set; }

        public double ValorInicialCaixa { get; set; }
    }
}