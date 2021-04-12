using System;
using Lab4_app.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab4_app.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
    }
}
