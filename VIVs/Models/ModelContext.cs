using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VIVs.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Vivsaboutu> Vivsaboutus { get; set; }
        public virtual DbSet<Vivsbooking> Vivsbookings { get; set; }
        public virtual DbSet<Vivscategory> Vivscategories { get; set; }
        public virtual DbSet<Vivscity> Vivscities { get; set; }
        public virtual DbSet<Vivscontactu> Vivscontactus { get; set; }
        public virtual DbSet<Vivshome> Vivshomes { get; set; }
        public virtual DbSet<Vivslogin> Vivslogins { get; set; }
        public virtual DbSet<Vivspost> Vivsposts { get; set; }
        public virtual DbSet<Vivsrole> Vivsroles { get; set; }
        public virtual DbSet<Vivstestimonial> Vivstestimonials { get; set; }
        public virtual DbSet<Vivsuser> Vivsusers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR17_User163;PASSWORD=Test321;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR17_USER163")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");


            modelBuilder.Entity<Vivsaboutu>(entity =>
            {
                entity.ToTable("VIVSABOUTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.PhoneNumber)
                    .HasPrecision(17)
                    .HasColumnName("PHONE_NUMBER");
            });

            modelBuilder.Entity<Vivsbooking>(entity =>
            {
                entity.HasKey(e => e.Bookid)
                    .HasName("SYS_C00368541");

                entity.ToTable("VIVSBOOKING");

                entity.Property(e => e.Bookid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BOOKID");

                entity.Property(e => e.Booktime)
                    .HasPrecision(6)
                    .HasColumnName("BOOKTIME");

                entity.Property(e => e.Postid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("POSTID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Vivsbookings)
                    .HasForeignKey(d => d.Postid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("SYS_C00368542");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vivsbookings)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00368543");
            });

            modelBuilder.Entity<Vivscategory>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("SYS_C00368518");

                entity.ToTable("VIVSCATEGORIES");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryimagepath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYIMAGEPATH");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");
            });

            modelBuilder.Entity<Vivscity>(entity =>
            {
                entity.HasKey(e => e.Cityid)
                    .HasName("SYS_C00368520");

                entity.ToTable("VIVSCITY");

                entity.Property(e => e.Cityid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CITYID");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CITY");
            });

            modelBuilder.Entity<Vivscontactu>(entity =>
            {
                entity.ToTable("VIVSCONTACTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.PhoneNumber)
                    .HasPrecision(17)
                    .HasColumnName("PHONE_NUMBER");
            });

            modelBuilder.Entity<Vivshome>(entity =>
            {
                entity.ToTable("VIVSHOME");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Image1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_1");

                entity.Property(e => e.Image2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_2");

                entity.Property(e => e.Image3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_3");

                entity.Property(e => e.Title1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TITLE_1");

                entity.Property(e => e.Title2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TITLE_2");

                entity.Property(e => e.Title3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TITLE_3");
            });

            modelBuilder.Entity<Vivslogin>(entity =>
            {
                entity.HasKey(e => e.Loginid)
                    .HasName("SYS_C00368534");

                entity.ToTable("VIVSLOGIN");

                entity.Property(e => e.Loginid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGINID");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Rolesid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLESID");

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Usersid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERSID");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.Vivslogins)
                    .HasForeignKey(d => d.Rolesid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00368536");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Vivslogins)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00368535");
            });

            modelBuilder.Entity<Vivspost>(entity =>
            {
                entity.HasKey(e => e.Postid)
                    .HasName("SYS_C00368538");

                entity.ToTable("VIVSPOST");

                entity.Property(e => e.Postid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("POSTID");

                entity.Property(e => e.Deadline)
                    .HasPrecision(6)
                    .HasColumnName("DEADLINE");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Isdeleted)
                    .HasPrecision(1)
                    .HasColumnName("ISDELETED");

                entity.Property(e => e.Numberofitem)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NUMBEROFITEM");

                entity.Property(e => e.Postdate)
                    .HasColumnType("DATE")
                    .HasColumnName("POSTDATE");

                entity.Property(e => e.Usersid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERSID");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Vivsposts)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00368539");
            });

            modelBuilder.Entity<Vivsrole>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("SYS_C00368522");

                entity.ToTable("VIVSROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Vivstestimonial>(entity =>
            {
                entity.ToTable("VIVSTESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Opinion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OPINION");

                entity.Property(e => e.Rating)
                    .HasPrecision(10)
                    .HasColumnName("RATING");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vivstestimonials)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00368546");
            });

            modelBuilder.Entity<Vivsuser>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("SYS_C00368530");

                entity.ToTable("VIVSUSERS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Categorytypeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORYTYPEID");

                entity.Property(e => e.Cityid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CITYID");

                entity.Property(e => e.Confirmpassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONFIRMPASSWORD");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Establishmentnationalnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ESTABLISHMENTNATIONALNUMBER");

                entity.Property(e => e.Estabname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ESTABNAME");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Isdeleted)
                    .HasPrecision(1)
                    .HasColumnName("ISDELETED");

                entity.Property(e => e.Otherscategory)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OTHERSCATEGORY");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Verifycode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VERIFYCODE");

                entity.HasOne(d => d.Categorytype)
                    .WithMany(p => p.Vivsusers)
                    .HasForeignKey(d => d.Categorytypeid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00368531");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Vivsusers)
                    .HasForeignKey(d => d.Cityid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("SYS_C00368532");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
