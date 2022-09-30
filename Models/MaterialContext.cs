using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace PlanIt.Models
{
    public class MaterialContext : DbContext
    {
        public MaterialContext(DbContextOptions<MaterialContext> options)
            : base(options)
        {
        }

        public DbSet<Material> Materials { get; set; } = null!;
    }
}