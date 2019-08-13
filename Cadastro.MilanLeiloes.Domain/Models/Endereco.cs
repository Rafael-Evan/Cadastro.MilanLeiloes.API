using Cadastro.MilanLeiloes.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cadastro.MilanLeiloes.Domain.Models
{
    public class Endereco
    {
        [Key]
        public virtual int Id { get; set; }
        [Required]
        public virtual string Rua { get; set; }
        public virtual int Numero { get; set; }
        public virtual string Complemento { get; set; }
        [Required]
        public virtual string Bairro { get; set; }
        [Required]
        public virtual string Cidade { get; set; }
        [Required]
        public virtual string Estado { get; set; }
        [Required]
        public virtual string CEP { get; set; }
        public virtual int UserId { get; set; }

        public User User { get; set; }
    }
}
