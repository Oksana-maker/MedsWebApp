using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public class MedicineInPharmacyRepository : BaseRepository<MedicineInPharmacy>
    {
        public MedicineInPharmacyRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public Task<MedicineInPharmacy[]> FindByIds(int[] ids) =>
            Get(mip => ids.Contains(mip.Id))
            .ToArrayAsync();

        public Task<MedicineInPharmacy[]> FindByIdsEager(int[] ids) =>
            Get(mip => ids.Contains(mip.Id), false)
                .Include(med => med.Discount)
                .Include(med => med.Medicine)
                .Include(med => med.Pharmacy)
            .ToArrayAsync();
        public Task<MedicineInPharmacy> FindByIdEager(int id) =>
            Get(mip => mip.Id == id, false)
                .Include(med => med.Discount)
                .Include(med => med.Medicine)
                .Include(med => med.Pharmacy)
            .FirstOrDefaultAsync();

        public Task<MedicineInPharmacy[]> FindByDiscountIdEager(int discountId) =>
            Get(mip => mip.DiscountId == discountId,
                true)
                .Include(med => med.Pharmacy)
                .Include(med => med.Discount)
                .Include(mip => mip.Medicine)
                    .ThenInclude(m => m.Category)
                .Include(mip => mip.Medicine)
                    .ThenInclude(m => m.Manufacturer)
            .ToArrayAsync();

        public async Task<(MedicineInPharmacy[] medicines, int totalPages)> GetAll(int page, int pageSize)
        {
            if (page < 1) page = 1;
            var source = Get(null, false)
                .Include(med => med.Discount)
                .Include(med => med.Medicine)
                .Include(med => med.Pharmacy).OrderBy(m => m.Medicine.Name);
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
