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
    public class SportService
    {
        private readonly Guid _userId;
        public SportService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateSport(SportCreate model)
        {
            var entity =
                new Sport()
                {
                    //OwnerId = _userId,
                    SportName = model.SportName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Sports.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<SportListItem> GetSport()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Sports
                        .Select(
                            e =>
                                new SportListItem
                                {
                                    SportId = e.SportId,
                                    SportName = e.SportName
                                }

                        );
                return query.ToArray();
            }
        }
    }
}
