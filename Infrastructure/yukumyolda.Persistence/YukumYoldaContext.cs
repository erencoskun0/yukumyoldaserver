using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Domain.Entities;
using System.Reflection.Emit;

namespace yukumyolda.Persistence
{
    public class YukumYoldaContext : IdentityDbContext<User, Role, Guid>
    {
        // Constructor for dependency injection
        public YukumYoldaContext(DbContextOptions<YukumYoldaContext> options) : base(options)
        {
        }

        // Parameterless constructor for design-time operations (migrations)
        public YukumYoldaContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=YukumYolda;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadStatus> LoadStatuses { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<UserVehicle> UserVehicles { get; set; }
        public DbSet<UserLoad> UserLoads { get; set; }

        public DbSet<VehicleBody> VehicleBodies { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

     
            builder.Entity<User>(entity =>
            {
                // User tablosu için temel ayarlar
                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.RefreshToken)
                    .HasMaxLength(1000);

                // Email opsiyonel olabilir
                entity.Property(u => u.Email)
                    .HasMaxLength(256);

                // Telefon numarası zorunlu
                entity.Property(u => u.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                // Telefon numarası unique olmalı
                entity.HasIndex(u => u.PhoneNumber)
                    .IsUnique();

                // UserName opsiyonel
                entity.Property(u => u.UserName)
                    .HasMaxLength(256);
            });

            // Vehicle Entity Konfigürasyonu  
            builder.Entity<Vehicle>(entity =>
            {
                // Plaka unique olmalı
                entity.HasIndex(v => v.Plate)
                    .IsUnique();

                entity.Property(v => v.Plate)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(v => v.TrailerPlate)
                    .HasMaxLength(20);

                // UserVehicle ile 1:1 ilişki
                entity.HasOne(v => v.UserVehicle)
                    .WithOne(uv => uv.Vehicle)
                    .HasForeignKey<UserVehicle>(uv => uv.VehicleId);

                // VehicleType ile ilişki
                entity.HasOne(v => v.VehicleType)
                    .WithMany(vt => vt.Vehicles)
                    .HasForeignKey(v => v.VehicleTypeId);

                // VehicleBody ile ilişki  
                entity.HasOne(v => v.VehicleBody)
                    .WithMany(vb => vb.Vehicles)
                    .HasForeignKey(v => v.VehicleBodyId);
            });
            // VehicleType Entity Konfigürasyonu
            builder.Entity<VehicleType>(entity =>
            {
                entity.Property(vt => vt.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(vt => vt.IconUrl)
                    .HasMaxLength(500);

                // TypeName unique olmalı
                entity.HasIndex(vt => vt.TypeName)
                    .IsUnique();
            });

            // VehicleBody Entity Konfigürasyonu  
            builder.Entity<VehicleBody>(entity =>
            {
                entity.Property(vb => vb.BodyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(vb => vb.IconUrl)
                    .HasMaxLength(500);

                // BodyName unique olmalı
                entity.HasIndex(vb => vb.BodyName)
                    .IsUnique();
            });

            // Load Entity Konfigürasyonu
            builder.Entity<Load>(entity =>
            {
                entity.Property(l => l.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                // UserLoad ile ilişki (Bir Load birden fazla UserLoad kaydıyla ilişkili olabilir)
                entity.HasMany(l => l.UserLoads)
                    .WithOne(ul => ul.Load)
                    .HasForeignKey(ul => ul.LoadId)
                    .OnDelete(DeleteBehavior.Cascade); // Load silinirse ilişkili UserLoad kayıtları da silinsin

                // LoadStatus ile ilişki
                entity.HasOne(l => l.LoadStatus)
                    .WithMany(ls => ls.Loads)
                    .HasForeignKey(l => l.LoadStatusId);

                // Kalkış ili ile ilişki
                entity.HasOne(l => l.DepartureProvince)
                    .WithMany(p => p.DepartureLoads)
                    .HasForeignKey(l => l.DeparturevId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Varış ili ile ilişki  
                entity.HasOne(l => l.DestinationProvince)
                    .WithMany(p => p.DestinationLoads)
                    .HasForeignKey(l => l.DestinationProvinceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // LoadStatus Entity Konfigürasyonu
            builder.Entity<LoadStatus>(entity =>
            {
                entity.Property(ls => ls.StateName)
                    .IsRequired()
                    .HasMaxLength(50);

                // StateName unique olmalı
                entity.HasIndex(ls => ls.StateName)
                    .IsUnique();
            });

            // Province Entity Konfigürasyonu
            builder.Entity<Province>(entity =>
            {
                entity.Property(p => p.ProvinceName)
                    .IsRequired()
                    .HasMaxLength(100);

                // ProvinceName unique olmalı
                entity.HasIndex(p => p.ProvinceName)
                    .IsUnique();
            });

            // Evaluation Entity Konfigürasyonu - Mevcut basit konfigürasyonu genişletiyoruz
            builder.Entity<Evaluation>(entity =>
            {
                // Tablo adı ve check constraint'leri
                entity.ToTable("Evaluations", t =>
                {
                    t.HasCheckConstraint("CK_Evaluation_SelfEvaluation",
                        "[EvaluatorUserId] <> [EvaluatedUserId]");
                });

                entity.Property(e => e.Comment)
                    .HasMaxLength(500);

                // Rating 0-5 arasında olmalı
                entity.Property(e => e.Rating)
                    .IsRequired();

                // Load ile ilişki
                entity.HasOne(e => e.Load)
                    .WithMany(l => l.Evaluations)
                    .HasForeignKey(e => e.LoadId);

                // Bir kullanıcı aynı yük için sadece bir değerlendirme yapabilir
                entity.HasIndex(e => new { e.EvaluatorUserId, e.LoadId })
                    .IsUnique();
            });

            builder.Entity<Evaluation>()
                .HasOne(e => e.EvaluatorUser)
                .WithMany(u => u.GivenEvaluations)
                .HasForeignKey(e => e.EvaluatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Evaluation>()
                .HasOne(e => e.EvaluatedUser)
                .WithMany(u => u.ReceivedEvaluations)
                .HasForeignKey(e => e.EvaluatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserLoad>(entity =>
            {
                entity.HasKey(ul => new { ul.UserId, ul.LoadId });

                entity.HasOne(ul => ul.User)
                    .WithMany(u => u.UserLoads)
                    .HasForeignKey(ul => ul.UserId)
                    .OnDelete(DeleteBehavior.Restrict);  // veya DeleteBehavior.NoAction

                entity.HasOne(ul => ul.Load)
                    .WithMany(l => l.UserLoads)
                    .HasForeignKey(ul => ul.LoadId)
                    .OnDelete(DeleteBehavior.Restrict);  // veya DeleteBehavior.NoAction
            });

            builder.Entity<UserVehicle>(entity =>
            { 
                entity.HasKey(uv => uv.VehicleId);
                 
                entity.HasOne(uv => uv.Vehicle)
                    .WithOne(v => v.UserVehicle)
                    .HasForeignKey<UserVehicle>(uv => uv.VehicleId)
                    .IsRequired()  
                    .OnDelete(DeleteBehavior.Cascade);
                 
                entity.HasOne(uv => uv.User)
                    .WithOne(u => u.UserVehicle)
                    .HasForeignKey<UserVehicle>(uv => uv.UserId)
                    .IsRequired()  
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed Data - Başlangıç Verileri
            SeedInitialData(builder);
        }

        private void SeedInitialData(ModelBuilder builder)
        {
            builder.Entity<VehicleType>().HasData(
                new VehicleType { Id = 1, TypeName = "Tır" },
                new VehicleType { Id = 2, TypeName = "Kamyon" },
                new VehicleType { Id = 3, TypeName = "Kamyonet" }
            );

            builder.Entity<VehicleBody>().HasData(
                 new VehicleBody { Id = 1, BodyName = "Kapalı Kasa" },
                 new VehicleBody { Id = 2, BodyName = "Açık Kasa" },
                 new VehicleBody { Id = 3, BodyName = "Tenteli" },
                 new VehicleBody { Id = 4, BodyName = "Soğutucu" },
                 new VehicleBody { Id = 5, BodyName = "Tanker" },
                 new VehicleBody { Id = 6, BodyName = "Damperli" },
                 new VehicleBody { Id = 7, BodyName = "Platform" },
                 new VehicleBody { Id = 8, BodyName = "Konteyner Taşıyıcı" },
                 new VehicleBody { Id = 9, BodyName = "Lowbed" },
                 new VehicleBody { Id = 10, BodyName = "Silobas" },
                 new VehicleBody { Id = 11, BodyName = "Frigofrik" },
                 new VehicleBody { Id = 12, BodyName = "Canlı Hayvan Taşıyıcı" },
                 new VehicleBody { Id = 13, BodyName = "Araç Taşıyıcı" }
   );

            builder.Entity<LoadStatus>().HasData(
                new LoadStatus { Id = 1, StateName = "Araç Aranıyor" },
                new LoadStatus { Id = 2, StateName = "Araç Bulundu" },
                new LoadStatus { Id = 3, StateName = "İptal Edildi" }
            );

            builder.Entity<Province>().HasData(
                new Province { Id = 1, ProvinceName = "Adana" },
                new Province { Id = 2, ProvinceName = "Adıyaman" },
                new Province { Id = 3, ProvinceName = "Afyonkarahisar" },
                new Province { Id = 4, ProvinceName = "Ağrı" },
                new Province { Id = 5, ProvinceName = "Amasya" },
                new Province { Id = 6, ProvinceName = "Ankara" },
                new Province { Id = 7, ProvinceName = "Antalya" },
                new Province { Id = 8, ProvinceName = "Artvin" },
                new Province { Id = 9, ProvinceName = "Aydın" },
                new Province { Id = 10, ProvinceName = "Balıkesir" },
                new Province { Id = 11, ProvinceName = "Bilecik" },
                new Province { Id = 12, ProvinceName = "Bingöl" },
                new Province { Id = 13, ProvinceName = "Bitlis" },
                new Province { Id = 14, ProvinceName = "Bolu" },
                new Province { Id = 15, ProvinceName = "Burdur" },
                new Province { Id = 16, ProvinceName = "Bursa" },
                new Province { Id = 17, ProvinceName = "Çanakkale" },
                new Province { Id = 18, ProvinceName = "Çankırı" },
                new Province { Id = 19, ProvinceName = "Çorum" },
                new Province { Id = 20, ProvinceName = "Denizli" },
                new Province { Id = 21, ProvinceName = "Diyarbakır" },
                new Province { Id = 22, ProvinceName = "Edirne" },
                new Province { Id = 23, ProvinceName = "Elazığ" },
                new Province { Id = 24, ProvinceName = "Erzincan" },
                new Province { Id = 25, ProvinceName = "Erzurum" },
                new Province { Id = 26, ProvinceName = "Eskişehir" },
                new Province { Id = 27, ProvinceName = "Gaziantep" },
                new Province { Id = 28, ProvinceName = "Giresun" },
                new Province { Id = 29, ProvinceName = "Gümüşhane" },
                new Province { Id = 30, ProvinceName = "Hakkâri" },
                new Province { Id = 31, ProvinceName = "Hatay" },
                new Province { Id = 32, ProvinceName = "Isparta" },
                new Province { Id = 33, ProvinceName = "Mersin" },
                new Province { Id = 34, ProvinceName = "İstanbul" },
                new Province { Id = 35, ProvinceName = "İzmir" },
                new Province { Id = 36, ProvinceName = "Kars" },
                new Province { Id = 37, ProvinceName = "Kastamonu" },
                new Province { Id = 38, ProvinceName = "Kayseri" },
                new Province { Id = 39, ProvinceName = "Kırklareli" },
                new Province { Id = 40, ProvinceName = "Kırşehir" },
                new Province { Id = 41, ProvinceName = "Kocaeli" },
                new Province { Id = 42, ProvinceName = "Konya" },
                new Province { Id = 43, ProvinceName = "Kütahya" },
                new Province { Id = 44, ProvinceName = "Malatya" },
                new Province { Id = 45, ProvinceName = "Manisa" },
                new Province { Id = 46, ProvinceName = "Kahramanmaraş" },
                new Province { Id = 47, ProvinceName = "Mardin" },
                new Province { Id = 48, ProvinceName = "Muğla" },
                new Province { Id = 49, ProvinceName = "Muş" },
                new Province { Id = 50, ProvinceName = "Nevşehir" },
                new Province { Id = 51, ProvinceName = "Niğde" },
                new Province { Id = 52, ProvinceName = "Ordu" },
                new Province { Id = 53, ProvinceName = "Rize" },
                new Province { Id = 54, ProvinceName = "Sakarya" },
                new Province { Id = 55, ProvinceName = "Samsun" },
                new Province { Id = 56, ProvinceName = "Siirt" },
                new Province { Id = 57, ProvinceName = "Sinop" },
                new Province { Id = 58, ProvinceName = "Sivas" },
                new Province { Id = 59, ProvinceName = "Tekirdağ" },
                new Province { Id = 60, ProvinceName = "Tokat" },
                new Province { Id = 61, ProvinceName = "Trabzon" },
                new Province { Id = 62, ProvinceName = "Tunceli" },
                new Province { Id = 63, ProvinceName = "Şanlıurfa" },
                new Province { Id = 64, ProvinceName = "Uşak" },
                new Province { Id = 65, ProvinceName = "Van" },
                new Province { Id = 66, ProvinceName = "Yozgat" },
                new Province { Id = 67, ProvinceName = "Zonguldak" },
                new Province { Id = 68, ProvinceName = "Aksaray" },
                new Province { Id = 69, ProvinceName = "Bayburt" },
                new Province { Id = 70, ProvinceName = "Karaman" },
                new Province { Id = 71, ProvinceName = "Kırıkkale" },
                new Province { Id = 72, ProvinceName = "Batman" },
                new Province { Id = 73, ProvinceName = "Şırnak" },
                new Province { Id = 74, ProvinceName = "Bartın" },
                new Province { Id = 75, ProvinceName = "Ardahan" },
                new Province { Id = 76, ProvinceName = "Iğdır" },
                new Province { Id = 77, ProvinceName = "Yalova" },
                new Province { Id = 78, ProvinceName = "Karabük" },
                new Province { Id = 79, ProvinceName = "Kilis" },
                new Province { Id = 80, ProvinceName = "Osmaniye" },
                new Province { Id = 81, ProvinceName = "Düzce" }
  );
        }
    }
}

