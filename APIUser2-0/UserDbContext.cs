using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIUser2_0
{
    public partial class UserDbContext : DbContext
    {
        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Subscribe> Subscribe { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=51.159.27.4;Port=35175;Database=UserDb;Username=ducklingSocial;Password=Epsi2019!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscribe>(entity =>
            {
                entity.HasKey(e => new { e.Subscriber, e.Subscribeto });

                entity.ToTable("subscribe");

                entity.HasIndex(e => e.Subscriber)
                    .HasName("fki_subscriber_fk");

                entity.HasIndex(e => e.Subscribeto)
                    .HasName("fki_subscribeto_fk");

                entity.Property(e => e.Subscriber).HasColumnName("subscriber");

                entity.Property(e => e.Subscribeto).HasColumnName("subscribeto");

                entity.HasOne(d => d.SubscriberNavigation)
                    .WithMany(p => p.SubscribeSubscriberNavigation)
                    .HasForeignKey(d => d.Subscriber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subscriber_fk");

                entity.HasOne(d => d.SubscribetoNavigation)
                    .WithMany(p => p.SubscribeSubscribetoNavigation)
                    .HasForeignKey(d => d.Subscribeto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subscribeto_fk");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users");

                entity.Property(e => e.UserId).ValueGeneratedNever();
            });
        }
    }
}
