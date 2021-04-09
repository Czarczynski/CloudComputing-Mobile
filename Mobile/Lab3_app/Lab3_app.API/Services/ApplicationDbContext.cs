using System;
using Lab3_app.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab3_app.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
    }
}
