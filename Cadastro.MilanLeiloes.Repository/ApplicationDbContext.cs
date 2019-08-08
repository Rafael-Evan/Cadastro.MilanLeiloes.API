using Cadastro.MilanLeiloes.Domain.Model;
using Cadastro.MilanLeiloes.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.MilanLeiloes.Repository
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int,
                                                        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<Telefone>(telefone =>
            {
                telefone.HasKey(ur => new { ur.Id});

                telefone.HasOne(ur => ur.User)
                .WithMany(r => r.Telefones)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<Endereco>(endereco =>
            {
                endereco.HasKey(ur => new { ur.Id });

                endereco.HasOne(ur => ur.User)
                .WithMany(r => r.Enderecos)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
        }

    }
}

