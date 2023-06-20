using apiplanoacao.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace apiplanoacao.Data.Map
{
    public class PlanoAcaoEntityTypeConfiguration:  IEntityTypeConfiguration<PlanoAcaoModel>
    {
        public void Configure(EntityTypeBuilder<PlanoAcaoModel> builder)
        {
            builder.ToTable("tb_planoacao");
            
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.usuario)
                .WithMany(p => p.planoAcao)
                .HasForeignKey(p => p.ColaboradorAprovador);

            builder.HasOne(p => p.usuario)
                .WithMany(p => p.planoAcao)
                .HasForeignKey(p => p.IdUsuario);

            builder.Property(p => p.DescricaoAcao)
                .HasColumnName("descricao_acao")
                .IsRequired()
                .HasColumnType("varchar(240)");

            builder.Property(p => p.DataInicio)
                .HasColumnName("data_inicio")
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(p => p.DataFim)
                .HasColumnName("data_fim")
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasColumnType("int");

            builder.HasMany(x => x.usuarios)
                .WithMany(x => x.planoAcao)
                .UsingEntity<Dictionary<string, object>>(
                    "usuario_planoacao",
                    j => j
                        .HasOne<UsuarioModel>()
                        .WithMany()
                        .HasForeignKey("IdPerfil"),

                    j => j
                        .HasOne<PlanoAcaoModel>()
                        .WithMany()
                        .HasForeignKey("IdMenu"));

        }
    }
}
