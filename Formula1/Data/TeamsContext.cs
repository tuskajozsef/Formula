using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
   public class TeamsContext: IdentityDbContext
    {
        public DbSet<Team> Teams { get; set; }

        public TeamsContext(DbContextOptions<TeamsContext> options)
           : base(options) {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
