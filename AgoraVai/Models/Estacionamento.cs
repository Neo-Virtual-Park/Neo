using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Estacionamento
    {
        [Key]
        public int Id { get; set; }
        
        public string nome { get; set; }
        public string estado { get; set; }
        public string bairro { get; set; }
        public string rua { get; set; }
        public string complemento { get; set; }
        public bool ativo { get; set; }

        public int GerenteId { get; set; }
        public virtual Gerente Gerente { get; set; }

        public int CustomizacoesId { get; set; }
        public virtual Customizacoes Customizacoes { get; set; }


    }
}