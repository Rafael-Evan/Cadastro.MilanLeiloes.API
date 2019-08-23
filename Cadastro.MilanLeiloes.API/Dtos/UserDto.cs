using Cadastro.MilanLeiloes.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cadastro.MilanLeiloes.API.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public int RG { get; set; }
        public virtual int? TelefoneResidencial { get; set; }
        public virtual int? TelefoneComercial { get; set; }
        public virtual string Celular { get; set; }
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
        public DateTime Data { get; set; }

    }
}
