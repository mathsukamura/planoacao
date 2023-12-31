﻿using apiplanoacao.Models;
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

            builder.HasOne(p => p.ColaboradorAprovador)
                .WithMany(p => p.PlanoacaoColaborador)
                .HasForeignKey(p => p.ColaboradorId); 

            builder.HasOne(p => p.Usuario)
                .WithMany(p => p.PlanoAcaos)
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

            builder.HasMany(x => x.ResponsaveisTratativa)
                .WithMany(x => x.PlanoAcaoResponsavel)
                .UsingEntity<Dictionary<string, object>>(
                    "planoAcao_responsavel",
                    j => j
                        .HasOne<UsuarioModel>()
                        .WithMany()
                        .HasForeignKey("id_responsavel"),

                    j => j
                        .HasOne<PlanoAcaoModel>()
                        .WithMany()
                        .HasForeignKey("id_planoacao"));
        }
    }
}
