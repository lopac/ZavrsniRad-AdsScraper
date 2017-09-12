namespace AdsScraper.DAL.Models
{
    using System.Data.Entity;

    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=DbConnection")
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Engine> Engines { get; set; }
        public virtual DbSet<Fuel> Fuels { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Engine>()
                .HasMany(e => e.Cars)
                .WithRequired(e => e.Engine)
                .HasForeignKey(e => e.Engine_Id);

            modelBuilder.Entity<Fuel>()
                .HasMany(e => e.Engines)
                .WithOptional(e => e.Fuel)
                .HasForeignKey(e => e.Fuel_Id);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.Model)
                .HasForeignKey(e => e.Model_Id);
        }
    }
}
