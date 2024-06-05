using CerealAPI.Contexts;
using CerealAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CerealAPI.Repositories
{
    public class CerealRepository : ICerealRepository
    {
        private static readonly CerealContext s_dbContext = new();
        private static readonly object s_lock = new();

        public async Task<CerealProduct?> GetCerealById(int id)
        {
            CerealProduct? cereal =
                await s_dbContext.FindAsync<CerealProduct>(id);

            return cereal;
        }

        public async Task<List<CerealProduct>> GetAllCereal()
        {
            List<CerealProduct> cereals =
                await s_dbContext.Cereals.ToListAsync();

            return cereals;
        }

        public async Task<bool> PostCereal(CerealProduct cereal)
        {
            await s_dbContext.AddAsync(cereal);
            int result = await s_dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateCereal(
            CerealProduct oldCereal, CerealProduct newCereal)
        {
            lock (s_lock)
            {
                s_dbContext.Entry(oldCereal).CurrentValues.SetValues(newCereal);
            }
            int result = await s_dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteCereal(CerealProduct cereal)
        {
            lock (s_lock)
            {
                s_dbContext.Remove(cereal);
            }
            int result = await s_dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
