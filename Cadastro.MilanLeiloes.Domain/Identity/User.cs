using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastro.MilanLeiloes.Domain.Model
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public string CPF { get; set; }
        public DateTime DataDeNascimento { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Sexo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string EstadoCivil { get; set; }
        public int RG { get; set; }
        public virtual int? TelefoneResidencial { get; set; }
        public virtual int? TelefoneComercial { get; set; }
        public virtual string Celular { get; set; }
        [Column(TypeName = "nvarchar(70)")]
        public virtual string Rua { get; set; }
        public virtual int Numero { get; set; }
        public virtual string Complemento { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(70)")]
        public virtual string Bairro { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(70)")]
        public virtual string Cidade { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public virtual string Estado { get; set; }
        [Required]
        [Column(TypeName = "varchar(8)")]
        public virtual string CEP { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
