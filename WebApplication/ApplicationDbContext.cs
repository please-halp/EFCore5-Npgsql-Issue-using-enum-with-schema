using Microsoft.EntityFrameworkCore;
using Npgsql;
using static WebApplication.Constants;

namespace WebApplication
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        static ApplicationDbContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<MyEnum>();


        public DbSet<MyEntity> MyEntity { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("schemaname");

            modelBuilder.HasPostgresEnum<MyEnum>("schemaname");

            // This solves the problem, but... There must be a better way!
            // Also, it doesn't work if the schema name contains a hyphen (e.g., "schema-name").
            //modelBuilder.Entity<MyEntity>()
            //    .Property(p => p.SomeProperty)
            //        .HasColumnType("schemaname.myenum"); // This should be managed by EF+Npg

            base.OnModelCreating(modelBuilder);
        }
    }
}
