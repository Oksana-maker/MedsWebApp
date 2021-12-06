using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class DiscountRepository : BaseRepository<Discount>
    {
        public DiscountRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public Task<Discount[]> GetActual() => Get(d => d.DiscountEnd > DateTime.Now && d.DiscountStart < DateTime.Now, noTracking: true).ToArrayAsync();
        public Task<Discount[]> GetAll() => Get(noTracking: true).ToArrayAsync();
        public async Task<(Discount[] discounts, int totalPages)> GetAll(int page, int pageSize)
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
        public Task<Discount> FindById(int id) =>
            Get(m => m.Id == id, noTracking: true)
            .FirstOrDefaultAsync();
    }
}
