using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using eLegal.Entities;

namespace eLegal.Data;

public partial class ELegalContext : DbContext
{
    public ELegalContext()
    {
    }

    public ELegalContext(DbContextOptions<ELegalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatCita> CatCitas { get; set; }

    public virtual DbSet<CatDepartamento> CatDepartamentos { get; set; }

    public virtual DbSet<CatEstatus> CatEstatuses { get; set; }

    public virtual DbSet<CatJerarquia> CatJerarquias { get; set; }

    public virtual DbSet<CatOrigen> CatOrigens { get; set; }

    public virtual DbSet<CatTipoEntradum> CatTipoEntrada { get; set; }

    public virtual DbSet<OprDetEntradum> OprDetEntrada { get; set; }

    public virtual DbSet<OprEntradasDepartamento> OprEntradasDepartamentos { get; set; }

    public virtual DbSet<OprEntradasUsuario> OprEntradasUsuarios { get; set; }

    public virtual DbSet<OprEntradum> OprEntrada { get; set; }

    public virtual DbSet<OprMedium> OprMedia { get; set; }

    public virtual DbSet<SysUsuario> SysUsuarios { get; set; }

    public virtual DbSet<VwCatCitasUsuario> VwCatCitasUsuarios { get; set; }

    public virtual DbSet<VwDetEntradum> VwDetEntrada { get; set; }

    public virtual DbSet<VwEntradasPersonal> VwEntradasPersonals { get; set; }

    public virtual DbSet<VwEntradasResuman> VwEntradasResumen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=eLegal");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<CatCita>(entity =>
        {
            entity.HasKey(e => e.CodigoCita).HasName("PK_CatCitas_codigoCita");

            entity.ToTable("Cat_Citas");

            entity.Property(e => e.CodigoCita)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("codigoCita");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.Fin)
                .HasColumnType("datetime")
                .HasColumnName("fin");
            entity.Property(e => e.FolioAsociado).HasColumnName("folioAsociado");
            entity.Property(e => e.Inactivo).HasColumnName("inactivo");
            entity.Property(e => e.Inicio)
                .HasColumnType("datetime")
                .HasColumnName("inicio");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");

            entity.HasOne(d => d.FolioAsociadoNavigation).WithMany(p => p.CatCita)
                .HasForeignKey(d => d.FolioAsociado)
                .HasConstraintName("FK_Cita_Entrada");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.CatCita)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK_cita_usuario");
        });

        modelBuilder.Entity<CatDepartamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Cat_Departamenots_Id");

            entity.ToTable("Cat_Departamentos");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<CatEstatus>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_Cat_Estatus_id")
                .IsClustered(false);

            entity.ToTable("Cat_Estatus");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[id_estatus])")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Inactivo)
                .HasDefaultValueSql("((0))")
                .HasColumnName("inactivo");
        });

        modelBuilder.Entity<CatJerarquia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cat_Jera__3213E83F8595EA32");

            entity.ToTable("Cat_Jerarquias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<CatOrigen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Origen_id");

            entity.ToTable("Cat_Origen");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[id_origen])")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
        });

        modelBuilder.Entity<CatTipoEntradum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cat_Tipo__3213E83FF9E49C66");

            entity.ToTable("Cat_TipoEntrada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Inactivo).HasColumnName("inactivo");
        });

        modelBuilder.Entity<OprDetEntradum>(entity =>
        {
            entity.HasKey(e => e.CodigoDetEntrada).HasName("PK_OprDetEntrada_CodigoDetEntrada");

            entity.ToTable("Opr_DetEntrada");

            entity.Property(e => e.CodigoDetEntrada)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("codigoDetEntrada");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.Observaciones)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.FolioNavigation).WithMany(p => p.OprDetEntrada)
                .HasForeignKey(d => d.Folio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetEntrada_Entrada");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.OprDetEntrada)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetEntrada_Departamento");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.OprDetEntrada)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetEntrada_Usuario");
        });

        modelBuilder.Entity<OprEntradasDepartamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Opr_Entr__3213E83F06AB75BA");

            entity.ToTable("Opr_EntradasDepartamentos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

            entity.HasOne(d => d.FolioNavigation).WithMany(p => p.OprEntradasDepartamentos)
                .HasForeignKey(d => d.Folio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntradasDepartamentos_Entrada");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.OprEntradasDepartamentos)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntradasDepartamentos_Usuarios");
        });

        modelBuilder.Entity<OprEntradasUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Opr_Entr__3213E83F8704AE8E");

            entity.ToTable("Opr_EntradasUsuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.FolioNavigation).WithMany(p => p.OprEntradasUsuarios)
                .HasForeignKey(d => d.Folio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntradasUsuarios_Entrada");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.OprEntradasUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntradasUsuarios_Usuarios");
        });

        modelBuilder.Entity<OprEntradum>(entity =>
        {
            entity.HasKey(e => e.Folio).HasName("PK_OprEntrada_Folio");

            entity.ToTable("Opr_Entrada");

            entity.Property(e => e.Folio)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[folio_entrada])")
                .HasColumnName("folio");
            entity.Property(e => e.Asunto).HasColumnName("asunto");
            entity.Property(e => e.EstatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estatus_id");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaConclucion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_conclucion");
            entity.Property(e => e.FechaOficio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_oficio");
            entity.Property(e => e.IdOrigen).HasColumnName("id_origen");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NumOficio).HasColumnName("num_oficio");
            entity.Property(e => e.OficinaOrigen).HasColumnName("oficina_origen");
            entity.Property(e => e.ReferenciaOrigen).HasColumnName("referencia_origen");
            entity.Property(e => e.TipoEntrada)
                .HasDefaultValueSql("((4))")
                .HasColumnName("tipoEntrada");

            entity.HasOne(d => d.Estatus).WithMany(p => p.OprEntrada)
                .HasForeignKey(d => d.EstatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Opr_Entrada_estatus_id");

            entity.HasOne(d => d.IdOrigenNavigation).WithMany(p => p.OprEntrada)
                .HasForeignKey(d => d.IdOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entrada_Origen");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.OprEntrada)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entrada_Usuario");

            entity.HasOne(d => d.TipoEntradaNavigation).WithMany(p => p.OprEntrada)
                .HasForeignKey(d => d.TipoEntrada)
                .HasConstraintName("FK_Entrada_Tipo_Entrada");
        });

        modelBuilder.Entity<OprMedium>(entity =>
        {
            entity.HasKey(e => e.CodigoDocumento).HasName("PK_OprMedia_codigoDocumento");

            entity.ToTable("Opr_Media");

            entity.Property(e => e.CodigoDocumento)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("codigoDocumento");
            entity.Property(e => e.Archivo).HasColumnName("archivo");
            entity.Property(e => e.CodigoDetEntrada).HasColumnName("codigoDetEntrada");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .HasColumnName("tipo");

            entity.HasOne(d => d.FolioNavigation).WithMany(p => p.OprMedia)
                .HasForeignKey(d => d.Folio)
                .HasConstraintName("FK_Media_Entrada");
        });

        modelBuilder.Entity<SysUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pk_SysUsuario_Id");

            entity.ToTable("Sys_Usuarios");

            entity.HasIndex(e => e.Usuario, "UQ__Sys_Usua__9AFF8FC637BD4B26").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[id_usuario])")
                .HasColumnName("id");
            entity.Property(e => e.Caducidad)
                .HasColumnType("datetime")
                .HasColumnName("caducidad");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.IdEmpleado)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("id_empleado");
            entity.Property(e => e.IdJerarquia)
                .HasDefaultValueSql("((4))")
                .HasColumnName("id_jerarquia");
            entity.Property(e => e.Inactivo).HasColumnName("inactivo");
            entity.Property(e => e.Mail)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Usuario)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.SysUsuarios)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_Usuario_Departamento");

            entity.HasOne(d => d.IdJerarquiaNavigation).WithMany(p => p.SysUsuarios)
                .HasForeignKey(d => d.IdJerarquia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Jerarquia");
        });

        modelBuilder.Entity<VwCatCitasUsuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_cat_citasUsuarios");

            entity.Property(e => e.CodigoCita).HasColumnName("codigoCita");
            entity.Property(e => e.DepartamentoId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("departamento_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.Fin)
                .HasColumnType("datetime")
                .HasColumnName("fin");
            entity.Property(e => e.FolioAsociado)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("folioAsociado");
            entity.Property(e => e.Inactivo).HasColumnName("inactivo");
            entity.Property(e => e.Inicio)
                .HasColumnType("datetime")
                .HasColumnName("inicio");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.Usuario)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("usuario");
            entity.Property(e => e.UsuarioCreacion)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("usuario_id");
        });

        modelBuilder.Entity<VwDetEntradum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_detEntrada");

            entity.Property(e => e.CodigoDetEntrada).HasColumnName("codigoDetEntrada");
            entity.Property(e => e.Departamento)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.Observaciones)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.Personal)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("personal");
        });

        modelBuilder.Entity<VwEntradasPersonal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_entradas_personal");

            entity.Property(e => e.Asunto).HasColumnName("asunto");
            entity.Property(e => e.DocumentosAdjuntos).HasColumnName("documentos_adjuntos");
            entity.Property(e => e.EventosRegistrados).HasColumnName("eventos_registrados");
            entity.Property(e => e.FechaOficioRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_oficio_registro");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Folio)
                .HasColumnType("bigint")
                .HasColumnName("folio");
            entity.Property(e => e.IdTipoEntrada).HasColumnName("id_tipo_entrada");
            entity.Property(e => e.IdTipoOrigen).HasColumnName("id_tipo_origen");
            entity.Property(e => e.NumeroOficioRegistro).HasColumnName("numero_oficio_registro");
            entity.Property(e => e.OficinaOrigen).HasColumnName("oficina_origen");
            entity.Property(e => e.ReferenciaRegistro).HasColumnName("referencia_registro");
            entity.Property(e => e.TipoEntrada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_entrada");
            entity.Property(e => e.TipoOrigen).HasColumnName("tipo_origen");
            entity.Property(e => e.UltimaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");
            entity.Property(e => e.UsuarioDepo)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("usuario_depo");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int")
                .HasColumnName("usuario_id");
            entity.Property(e => e.UsuarioIdDepo)
                .HasColumnType("int")
                .HasColumnName("usuario_id_depo");
            entity.Property(e => e.UsuarioJerarquia)
                .HasColumnType("int")
                .HasColumnName("usuario_jerarquia");
            entity.Property(e => e.UsuarioNombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("usuario_nombre");
        });

        modelBuilder.Entity<VwEntradasResuman>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_entradas_resumen");

            entity.Property(e => e.Asunto).HasColumnName("asunto");
            entity.Property(e => e.DepartamentoAsignados)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("departamento_asignados");
            entity.Property(e => e.DocumentosAdjuntos).HasColumnName("documentos_adjuntos");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.EstatusId).HasColumnName("estatus_id");
            entity.Property(e => e.EventosRegistrados).HasColumnName("eventos_registrados");
            entity.Property(e => e.FechaOficioRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_oficio_registro");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Folio)
                .HasColumnType("bigint")
                .HasColumnName("folio");
            entity.Property(e => e.IdDepartamentoAsignados)
                .HasMaxLength(4000)
                .HasColumnName("id_departamento_asignados");
            entity.Property(e => e.IdPersonalAsignados)
                .HasMaxLength(4000)
                .HasColumnName("id_personal_asignados");
            entity.Property(e => e.IdPersonalRegistro).HasColumnName("id_personal_registro");
            entity.Property(e => e.IdTipoEntrada).HasColumnName("id_tipo_entrada");
            entity.Property(e => e.IdTipoOrigen).HasColumnName("id_tipo_origen");
            entity.Property(e => e.NumeroOficioRegistro).HasColumnName("numero_oficio_registro");
            entity.Property(e => e.OficinaOrigen).HasColumnName("oficina_origen");
            entity.Property(e => e.PersonalAsignados)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("personal_asignados");
            entity.Property(e => e.PersonalRegistro)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("personal_registro");
            entity.Property(e => e.ReferenciaRegistro).HasColumnName("referencia_registro");
            entity.Property(e => e.TipoEntrada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_entrada");
            entity.Property(e => e.TipoOrigen).HasColumnName("tipo_origen");
            entity.Property(e => e.UltimaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");
        });
        modelBuilder.HasSequence<decimal>("folio_entrada")
            .HasMin(1L)
            .HasMax(9999999999L);
        modelBuilder.HasSequence<decimal>("id_departamento")
            .HasMin(1L)
            .HasMax(9999999999L);
        modelBuilder.HasSequence<decimal>("id_estatus")
            .HasMin(1L)
            .HasMax(9999999999L);
        modelBuilder.HasSequence<decimal>("id_media")
            .HasMin(1L)
            .HasMax(9999999999L);
        modelBuilder.HasSequence<decimal>("id_origen")
            .HasMin(1L)
            .HasMax(9999999999L);
        modelBuilder.HasSequence<decimal>("id_usuario")
            .HasMin(1L)
            .HasMax(9999999999L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
