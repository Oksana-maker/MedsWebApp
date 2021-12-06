using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class MedicineRepository : BaseRepository<Medicine>
    {
        public MedicineRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
        public Task<Medicine> FindById(int id) =>
            Get(model => model.Id == id).FirstOrDefaultAsync();
        public Task<Medicine> FindByIdEager(int id) =>
            Get(model => model.Id == id, true)
            .Include(med => med.Manufacturer)
            .Include(med => med.Category)
            .Include(med => med.MedicinesInPharmacy.Where(mp => mp.Active))
                .ThenInclude(m => m.Pharmacy)
            .Include(med => med.MedicinesInPharmacy.Where(mp => mp.Active))
                .ThenInclude(m => m.Discount)
            .FirstOrDefaultAsync();

        public async Task<(Medicine[] medicines, int totalPages)> FindByPartOfName(string query, int page, int pageSize)
        {
            if (page < 1) page = 1;
            var source = Get(med => med.Name.Contains(query), true);

            var totalCount = await source.CountAsync();
            var totalPages = (int)(totalCount <= pageSize ?
                1 :
                totalCount % pageSize > 0
                    ? Math.Truncate((double)totalCount / pageSize) + 1 :
                    Math.Truncate((double)totalCount / pageSize));
            var meds = await source
                .OrderBy(med => med.Name)
                .Skip(--page * pageSize)
                .Include(med => med.Manufacturer)
                .Include(med => med.Category)
                .Include(med => med.MedicinesInPharmacy.Where(mp => mp.Active))
                    .ThenInclude(m => m.Discount)
                .Take(pageSize)
                .ToArrayAsync();
            return (meds, totalPages);
        }
        public Task<Medicine[]> GetAll() => Get().ToArrayAsync();
        public Task<Medicine[]> FindByCategory(int categoryId) =>
            Get(med => med.CategoryId == categoryId,
                true)
            .Include(med => med.Manufacturer)
            .Include(med => med.Category)
            .Include(med => med.MedicinesInPharmacy.Where(mp => mp.Active))
                .ThenInclude(m => m.Discount)
            .OrderBy(med => med.Name)
            .ToArrayAsync();

        internal async Task<bool> HasMedicinesByCategory(int categoryId) =>
            (await Get(med => med.CategoryId == categoryId, false).CountAsync()) > 0;


        public async Task<(Medicine[] medicines, int totalPages)> GetAll(int page, int pageSize)
        {
            if (page < 1) page = 1;
            var source = Get(null, true)
                .Include(med => med.Manufacturer)
                .Include(med => med.Category)
                .OrderBy(m => m.Name);
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
