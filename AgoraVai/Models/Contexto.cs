using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgoraVai.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base(nameOrConnectionString: "StringConexao") { }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Vaga> Vaga { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Estacionamento> Estacionamento { get; set; }
        public DbSet<Gerente> Gerente { get; set; }
    }
}