using Microsoft.EntityFrameworkCore;
using NotaFiscalFaturamento.Domain.Entities;

namespace NotaFiscalFaturamento.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Nota> Notas { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
