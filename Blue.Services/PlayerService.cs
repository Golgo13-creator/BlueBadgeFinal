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
    public class PlayerService
    {
        private readonly Guid _userId;

        public PlayerService(Guid userId)
        {
            _userId = userId;
        }
        public bool PostPlayer(PlayerCreate model)
        {
            var entity = new Player()
            {
                OwnerId = _userId,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PlayerListItem> GetPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Players
                    .Select(p => new PlayerListItem
                    {
                        PlayerId = p.PlayerId,
                        FirstName = p.FirstName,
                        LastName = p.LastName
                    });

                return query.ToArray();
            }
        }
    }
}
