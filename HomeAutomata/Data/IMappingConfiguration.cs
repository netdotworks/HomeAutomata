using Microsoft.EntityFrameworkCore;

namespace HomeAutomata.Data
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}