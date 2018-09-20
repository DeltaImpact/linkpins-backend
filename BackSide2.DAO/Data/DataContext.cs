using System.Threading;
using System.Threading.Tasks;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Extentions;
using Microsoft.EntityFrameworkCore;

namespace BackSide2.DAO.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var defaultUser = new Person()
            //{
            //   // Id = 1,
            //    UserName = "system",
            //    Email = "system@admin.com",
            //    Password = null,
            //    Role = Role.Admin,
            //    FirstName = null,
            //    Surname = null,
            //    Gender = null,
            //    Language = null,
            //    CreatedBy = null,
            //    UpdatedBy = null,
            //};
            //modelBuilder.Entity<Person>().HasData(defaultUser);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.ApplyAuditInformation();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Pin> Pins { get; set; }
    }
}
