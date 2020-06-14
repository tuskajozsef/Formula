using Formula1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Services
{
    public interface ITeamService
    {
        Task<Team> GetTeamAsync(int teamId);
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> InsertTeamAsync(Team newTeam);
        Task UpdateTeamAsync(int teamId, Team updatedTeam);
        Task DeleteTeamAsync(int teamId);
        bool Exists(int id);
    }
}
