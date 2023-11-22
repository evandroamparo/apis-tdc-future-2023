using Microsoft.EntityFrameworkCore;
using PersonApi.Models;

namespace PersonApi.Data
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }
    }
}