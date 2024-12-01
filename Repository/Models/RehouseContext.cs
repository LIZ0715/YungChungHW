using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Yungching.Repository.Models;

public partial class RehouseContext : DbContext
{
    public RehouseContext()
    {
    }

    public RehouseContext(DbContextOptions<RehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estate> Estates { get; set; }

    public virtual DbSet<MemberShip> MemberShips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Rehouse;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estate>(entity =>
        {
            entity.ToTable("Estate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.MemberShipId).HasColumnName("memberShipId");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Range)
                .HasComment("坪數")
                .HasColumnName("range");
            entity.Property(e => e.Status)
                .HasComment("狀態 刪除false 存在true")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasComment("1=公寓 2=透天")
                .HasColumnName("type");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");

            entity.HasOne(d => d.MemberShip).WithMany(p => p.Estates)
                .HasForeignKey(d => d.MemberShipId)
                .HasConstraintName("FK_Estate_MemberShip");
        });

        modelBuilder.Entity<MemberShip>(entity =>
        {
            entity.ToTable("MemberShip");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(100)
                .HasColumnName("account");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
