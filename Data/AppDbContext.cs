using Microsoft.EntityFrameworkCore;
using TableManagement.Models;

namespace TableManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Reservation>()
                .HasIndex(r => new { r.TableId, r.ReservationDate })
                .IsUnique();

            modelBuilder
                .Entity<Table>()
                .HasData(
                    new Table
                    {
                        Id = 1,
                        TableCode = "A1",
                        Status = "Available",
                    },
                    new Table
                    {
                        Id = 2,
                        TableCode = "A2",
                        Status = "Available",
                    },
                    new Table
                    {
                        Id = 3,
                        TableCode = "A3",
                        Status = "Available",
                    },
                    new Table
                    {
                        Id = 4,
                        TableCode = "B1",
                        Status = "Available",
                    },
                    new Table
                    {
                        Id = 5,
                        TableCode = "B2",
                        Status = "Available",
                    },
                    new Table
                    {
                        Id = 6,
                        TableCode = "B3",
                        Status = "Available",
                    }
                );
        }
    }
}
