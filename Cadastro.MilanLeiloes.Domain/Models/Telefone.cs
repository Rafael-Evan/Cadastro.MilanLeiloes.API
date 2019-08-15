using Cadastro.MilanLeiloes.Domain.Model;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cadastro.MilanLeiloes.Domain.Models
{
    public class Telefone
    {
        public virtual int Id { get; set; }
        public virtual int? TelefoneResidencial { get; set; }
        public virtual int? TelefoneComercial { get; set; }
        public virtual string Celular { get; set; }
        public virtual int UserId { get; set; }

        public User User { get; set; }
    }
}
