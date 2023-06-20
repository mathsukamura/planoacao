﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using apiplanoacao.Data;

#nullable disable

namespace apiplanoacao.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20230620204710_alterpropertyintableplanoacao")]
    partial class alterpropertyintableplanoacao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PlanoAcao_Responsavel", b =>
                {
                    b.Property<int>("id_planoacao")
                        .HasColumnType("integer")
                        .HasColumnName("id_planoacao");

                    b.Property<int>("id_responsavel")
                        .HasColumnType("integer")
                        .HasColumnName("id_responsavel");

                    b.HasKey("id_planoacao", "id_responsavel");

                    b.HasIndex("id_responsavel");

                    b.ToTable("PlanoAcao_Responsavel");
                });

            modelBuilder.Entity("apiplanoacao.Models.PlanoAcaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ColaboradorAprovador")
                        .HasColumnType("integer")
                        .HasColumnName("colaborador_aprovador");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("timestamp")
                        .HasColumnName("data_fim");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp")
                        .HasColumnName("data_inicio");

                    b.Property<string>("DescricaoAcao")
                        .IsRequired()
                        .HasColumnType("varchar(240)")
                        .HasColumnName("descricao_acao");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    b.Property<int>("ResponsavelTratativa")
                        .HasColumnType("integer")
                        .HasColumnName("responsavel_tratativa");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorAprovador");

                    b.HasIndex("IdUsuario");

                    b.ToTable("tb_planoacao", (string)null);
                });

            modelBuilder.Entity("apiplanoacao.Models.UsuarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(64)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("Varchar(24)")
                        .HasColumnName("nome");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("Varchar(24)")
                        .HasColumnName("senha");

                    b.HasKey("Id");

                    b.ToTable("tb_usuario", (string)null);
                });

            modelBuilder.Entity("PlanoAcao_Responsavel", b =>
                {
                    b.HasOne("apiplanoacao.Models.UsuarioModel", null)
                        .WithMany()
                        .HasForeignKey("id_planoacao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiplanoacao.Models.PlanoAcaoModel", null)
                        .WithMany()
                        .HasForeignKey("id_responsavel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("apiplanoacao.Models.PlanoAcaoModel", b =>
                {
                    b.HasOne("apiplanoacao.Models.UsuarioModel", "Colaboradoraprovador")
                        .WithMany("PlanoacaoColaborador")
                        .HasForeignKey("ColaboradorAprovador")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("apiplanoacao.Models.UsuarioModel", "Usuario")
                        .WithMany("PlanoAcaos")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Colaboradoraprovador");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("apiplanoacao.Models.UsuarioModel", b =>
                {
                    b.Navigation("PlanoAcaos");

                    b.Navigation("PlanoacaoColaborador");
                });
#pragma warning restore 612, 618
        }
    }
}
