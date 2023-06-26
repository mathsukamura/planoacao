using apiplanoacao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace apiplanoacao.Data.Map
{
    public class UsuarioEntityTypeConfiguration: IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("tb_usuario");

            builder.Property(s => s.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType("Varchar(100)");

            builder.Property(s => s.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasColumnType("Varchar(64)");

            builder.Property(s => s.Senha)
                .HasColumnName("senha")
                .IsRequired()
                .HasColumnType("Varchar(24)");
        }
    }
}
