using Blue.Data;
using Blue.Models;
using Blue.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.Services
{
    public class TeamService
    { 
        private readonly Guid _userId;
        public TeamService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateTeam(TeamCreate model)
        {
            var entity =
                new Team()
                {
                    OwnerId = _userId,
                    TeamName = model.TeamName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Teams.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TeamListItem> GetTeams()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Teams
                        .Select(
                            e =>
                                new TeamListItem
                                {
                                    TeamId = e.TeamId,
                                    TeamName = e.TeamName
                                }
                        );
                return query.ToArray();
            }
        }
    }
}
