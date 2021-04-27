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
        public bool PutPlayers(PlayerEdit newPlayerData)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldPlayerData =
                    ctx
                    .Players
                    .SingleOrDefault(p => p.PlayerId == newPlayerData.PlayerId);

                oldPlayerData.PlayerId = newPlayerData.PlayerId;
                oldPlayerData.FirstName = newPlayerData.FirstName;
                oldPlayerData.LastName = newPlayerData.LastName;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeletePlayers(int playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var playerToDelete =
                    ctx
                    .Players
                    .Single(p => p.PlayerId == playerId);

                if (playerToDelete != null)
                {
                    ctx.Players.Remove(playerToDelete);

                    return ctx.SaveChanges() == 1;
                }
                return false;
            }
        }
    }
}
