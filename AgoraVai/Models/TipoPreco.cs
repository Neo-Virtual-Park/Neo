using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class TipoPreco
    {
        [Key]
        public int Id { get; set; }

        public string Tipo { get; set; }

    }
}