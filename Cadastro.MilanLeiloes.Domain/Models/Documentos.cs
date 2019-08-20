using Cadastro.MilanLeiloes.Domain.Model;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cadastro.MilanLeiloes.Domain.Models
{
    public class Documentos
    {
    //    public virtual int DocumentoId { get; set; }
        public virtual string Id { get; set; }
        public virtual string Extension { get; set; }
        public virtual string Name { get; set; }
        public virtual string Source { get; set; }
        public virtual string Type { get; set; }
        public virtual string UploadURL { get; set; }
        //public virtual int UserId { get; set; }
        //public User User { get; set; }

    }
}
