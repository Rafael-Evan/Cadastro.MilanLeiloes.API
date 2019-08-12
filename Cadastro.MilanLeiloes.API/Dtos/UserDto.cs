using Cadastro.MilanLeiloes.Domain.Model;
using Cadastro.MilanLeiloes.Domain.Models;
using System;
using System.Collections.Generic;

namespace Cadastro.MilanLeiloes.API.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public int RG { get; set; }
        public int TelefoneResidencial { get; set; }
        public int TelefoneComercial { get; set; }
        public int Celular { get; set; }

    }
}
