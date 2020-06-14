using Formula1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formula1.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamService : ITeamService
    {
        private readonly TeamsContext _context;
        private static bool init = false;

        public TeamService(TeamsContext context)
        {
            _context = context;
            if(!_context.Teams.Any() && !init)
                SeedDatabase(_context);
        }

        static void SeedDatabase(TeamsContext ctx)
        {
            Team ferrari = new Team
            {
                Name = "Ferrari",
                FoundedIn = 1899,
                CheckPaid = false,
                ChampionshipsWon = 12
            };

            Team mclaren = new Team
            {
                Name = "McLaren",
                FoundedIn = 1910,
                CheckPaid = true,
                ChampionshipsWon = 3
            };

            Team mercedes = new Team
            {
                Name = "Mercedes-Benz",
                FoundedIn = 1960,
                CheckPaid = true,
                ChampionshipsWon = 25
            };

            //Add admin 
            var user = new IdentityUser("admin");
            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, ctx.GetService<IConfiguration>().GetSection("Password").Value);
            var userStore = ctx.GetService<UserManager<IdentityUser>>();
            userStore.CreateAsync(user);

            ctx.Add(ferrari);
            ctx.Add(mercedes);
            ctx.Add(mclaren);

            ctx.SaveChanges();

            init = true;
        }
            public async Task DeleteTeamAsync(int teamId)
        {
            _context.Teams.Remove(new Team { Id = teamId });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _context.Teams
                .SingleOrDefaultAsync(e => e.Id == teamId)) == null)
                    throw new Exception("Team not found");
                else throw;
            }
        }

        public bool Exists(int id)
        {
            return _context.Teams.Any(t => t.Id == id);
        }

        public async Task<Team> GetTeamAsync(int teamId)
        {
            return (await _context.Teams
               .SingleOrDefaultAsync(t => t.Id == teamId))
               ?? throw new Exception("Team not found");
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            var teams = await _context.Teams
            .ToListAsync();

            return teams;
        }

        public async Task<Team> InsertTeamAsync(Team newTeam)
        {
            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync();
            return newTeam;
        }

        public async Task UpdateTeamAsync(int teamId, Team updatedTeam)
        {
            updatedTeam.Id = teamId;
            var entry = _context.Attach(updatedTeam);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _context.Teams
                        .SingleOrDefaultAsync(p => p.Id == teamId)) == null)
                    throw new Exception("Team not found");
                else throw;
            }
        }

    }
}
