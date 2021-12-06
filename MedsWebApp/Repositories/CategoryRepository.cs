using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public Task<Category[]> GetRootCategories() => 
            Get(cc => cc.ParentId == null, true)
            .OrderBy(cc => cc.Name)
            .ToArrayAsync();

        public Task<Category[]> GetChildren(int parentId) =>
            Get(cc => cc.ParentId == parentId, true)
            .OrderBy(cc => cc.Name)
            .ToArrayAsync();

        public Task<Category[]> GetAll() =>
            Get(noTracking: true)
            .OrderBy(cc => cc.Name)
            .ToArrayAsync();

        public async Task<(Category[] categories, int totalPages)> GetAll(int page, int pageSize)
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
        public Task<Category> FindById(int id) =>
            Get(m => m.Id == id, noTracking: true)
            .FirstOrDefaultAsync();

    }
}
