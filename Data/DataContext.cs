using Microsoft.EntityFrameworkCore;
using dotNETCoreWebAPI.Models;

namespace dotNETCoreWebAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }

        public DbSet<Character> characters{get;set;}
        
    }
}