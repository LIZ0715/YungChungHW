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
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Rehouse;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estate>(entity =>
        {
            entity.ToTable("Estate");

            entity.Property(e => e.Id)
                .HasComment("流水號")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasComment("地址")
                .HasColumnName("address");
            entity.Property(e => e.CreateAt)
                .HasComment("新增時間")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.MemberShipId)
                .HasComment("會員Id")
                .HasColumnName("memberShipId");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasComment("物件名稱")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasComment("價格")
                .HasColumnName("price");
            entity.Property(e => e.Range)
                .HasComment("坪數")
                .HasColumnName("range");
            entity.Property(e => e.Status)
                .HasComment("false=停售  true=銷售中")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasComment("1=公寓 2=透天")
                .HasColumnName("type");
            entity.Property(e => e.UpdateAt)
                .HasComment("修改時間")
                .HasColumnType("datetime")
                .HasColumnName("updateAt");
        });

        modelBuilder.Entity<MemberShip>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MemberShip");

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
