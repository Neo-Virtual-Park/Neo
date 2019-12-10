using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Vaga
    {
        [Key]
        public int Id { get; set; }

        public string Tipo { get; set; }

        public decimal valor { get; set; }

        public decimal ValorMensal { get; set; }

        public int Quantidade { get; set; }

        public int QuantidadeHorasEspeciais { get; set; }

        public decimal valorHorasEspeciais { get; set; }

        public int EstacionamentoId { get; set; }
        public virtual Estacionamento Estacionamento { get; set; }

    }
}