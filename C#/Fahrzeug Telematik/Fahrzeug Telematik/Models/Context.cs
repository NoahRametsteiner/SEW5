using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Fahrzeug_Telematik.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Telemetry> Telemetrys { get; set; } = null!;
    }
}