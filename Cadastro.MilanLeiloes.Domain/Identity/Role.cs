using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cadastro.MilanLeiloes.Domain.Model
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
