using System;
using Lab1_czarczynski.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_czarczynski.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Person> People{ get; set; }
    }
}
