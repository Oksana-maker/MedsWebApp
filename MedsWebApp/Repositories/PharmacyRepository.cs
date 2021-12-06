using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class PharmacyRepository : BaseRepository<Pharmacy>
    {
        public PharmacyRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public Task<Pharmacy> FindById(int Id) =>
            Get(p => p.Id == Id).Include(nameof(Pharmacy.Users)).FirstOrDefaultAsync();
        public Task<Pharmacy[]> GetAll() => Get().ToArrayAsync();
        public async Task<(Pharmacy[] pharmacies, int totalPages)> GetAll(int page, int pageSize)
        {
            if (page < 1) page = 1;
            var source = Get(noTracking: true).OrderBy(m => m.Name);
            var totalCount = await source.CountAsync();
            var result = await source.Skip(--page * pageSize).Take(pageSize).ToArrayAsync();
            var totalPages = (int)(totalCount <= pageSize ?
                1 :
                totalCount % pageSize > 0
                    ? Math.Truncate((double)totalCount / pageSize) + 1 :
                    Math.Truncate((double)totalCount / pageSize));
            return (result, totalPages);
        }
    }
}
