﻿using AuthenticationService.Data.Properties;
using AuthenticationService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Data.Context
{
    public class AuthenticationContext : DbContext
    {
        #region constants
        private const string USUARIO_TABLE_NAME = "Usuarios";
        private const string TELEFONE_TABLE_NAME = "Telefones";
        #endregion

        #region configurations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Resources.DbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(us =>
            {
                us.HasKey(u => u.Id);

                us.Property(u => u.DataCriacao).HasColumnName("data_criacao");
                us.Property(u => u.UltimoLogin).HasColumnName("ultimo_logon");
                us.Property(u => u.DataAtualizacao).HasColumnName("data_atualizacao");
                us.Property(u => u.Id).HasColumnType("nvarchar(50)");
                us.Property(u => u.Nome).HasColumnType("varchar(50)");
                us.Property(u => u.Email).IsUnicode().HasColumnType("varchar(50)");
                us.Property(u => u.Senha).HasColumnType("varchar(140)");
                us.Property(u => u.Token).HasColumnType("varchar(140)");
                us.HasMany(u => u.Telefones).WithOne(t => t.User);
                us.ToTable<Usuario>(USUARIO_TABLE_NAME);            
            });

            modelBuilder.Entity<Telefone>(us =>
            {
                us.HasKey(t => t.Id);
                
                us.HasOne(t => t.User)
                .WithMany(u => u.Telefones)
                .HasForeignKey(t => t.UsuarioFK);

                us.Property(t => t.UsuarioFK).HasColumnName("usuario_id");
                us.ToTable<Telefone>(TELEFONE_TABLE_NAME);            
            });

        }
        #endregion

        #region DBSets
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Telefone> Telefones { get; set; }
        #endregion
    }
}
