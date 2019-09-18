using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Movimentacao
    {
        [Key]
        public int Id { get; set; }
        public string Cpf { get; set; }
        public int Telefone { get; set; }
        public string Placa { get; set; }
        public DateTime hora_entrada { get; set; }
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public DateTime? Hora_saida { get; set; }
        public float Valor_pagar { get; set; }
        public int VagaId { get; set; }
        public virtual Vaga Vaga { get; set; }
    }
}