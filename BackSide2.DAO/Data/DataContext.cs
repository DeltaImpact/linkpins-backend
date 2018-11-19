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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
