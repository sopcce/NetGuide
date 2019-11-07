
using DIAndPipe.Entities;
using Microsoft.EntityFrameworkCore;

namespace DIAndPipe
{
    public class EFContext:DbContext
    {
        
        public EFContext(DbContextOptions<EFContext> options):base(options)
        {
            
        }
    
        public DbSet<Person> Persons { get; set; }

        
    }
}