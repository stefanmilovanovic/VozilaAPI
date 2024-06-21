using Microsoft.EntityFrameworkCore;
using Vozila.Models;

namespace Vozila.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }  
        public DbSet<Proizvodjac> Proizvodjaci { get; set; }   
        public DbSet<Recenzija> Recenzije {get; set; }
        public DbSet<Recezent> Recezenti { get; set; }
        public DbSet<Vozilo> Vozila { get; set; }
        public DbSet<VoziloKategorija> VoziloKategorije { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VoziloKategorija>()
                .HasKey(vk => new { vk.VoziloId, vk.KategorijaId });
            modelBuilder.Entity<VoziloKategorija>()
                .HasOne(vk => vk.Kategorija)
                .WithMany(k => k.VoziloKategorije)
                .HasForeignKey(vk => vk.KategorijaId);
            modelBuilder.Entity<VoziloKategorija>()
                .HasOne(vk=>vk.Vozilo)
                .WithMany(v=>v.VoziloKategorije)
                .HasForeignKey(vk=>vk.VoziloId);
        }
    }
}
