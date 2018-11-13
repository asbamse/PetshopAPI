using Bamz.Petshop.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bamz.Petshop.Infrastructure.Data.Context
{
    public class PetshopContext : DbContext
    {
        public PetshopContext(DbContextOptions<PetshopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetColourRelation>().HasKey(pc => new { pc.PetId, pc.ColourId });

            modelBuilder.Entity<PetColourRelation>()
                .HasOne<Pet>(pc => pc.Pet)
                .WithMany(c => c.Colours)
                .HasForeignKey(pc => pc.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PetColourRelation>()
                .HasOne<Colour>(pc => pc.Colour)
                .WithMany()
                .HasForeignKey(pc => pc.ColourId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Pet>()
                .HasOne<PetType>(p => p.Type)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pet>()
                .HasOne<Person>(p => p.PreviousOwner)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Person>()
                .HasOne<Address>(p => p.Address)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne<Person>(o => o.Customer)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderPetRelation>().HasKey(ol => new { ol.OrderId, ol.PetId });

            modelBuilder.Entity<OrderPetRelation>()
                .HasOne<Order>(ol => ol.Order)
                .WithMany(o => o.Pets)
                .HasForeignKey(ol => ol.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<OrderPetRelation>()
                .HasOne<Pet>(ol => ol.Pet)
                .WithMany()
                .HasForeignKey(ol => ol.PetId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
    }
}
