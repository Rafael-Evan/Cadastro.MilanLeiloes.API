using Cadastro.MilanLeiloes.Domain.Model;
using System;

namespace Cadastro.MilanLeiloes.Domain.Models
{
    public class Documentos
    {
        public virtual int DocumentoId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Pasta { get; set; }
        public DateTime Data { get; set; }
        public virtual int UserId { get; set; }
        public User User { get; set; }

    }
}
