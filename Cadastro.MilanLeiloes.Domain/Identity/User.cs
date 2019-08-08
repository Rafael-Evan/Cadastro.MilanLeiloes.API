using Cadastro.MilanLeiloes.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastro.MilanLeiloes.Domain.Model
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public string CPF { get; set; }

        public DateTime DataDeNascimento { get; set; }

        public string Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public int RG { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<Telefone> Telefones { get; set; }

        public List<Endereco> Enderecos { get; set; }
    }
}
