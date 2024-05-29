using CerealAPI.Contexts;
using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public class CerealRepository : ICerealRepository
    {
        private static readonly CerealContext _dbContext = new();

        public async Task<CerealProduct?> GetCerealById(int id)
        {
            var cereal = await _dbContext.FindAsync<CerealProduct>(id);

            return cereal;
        }

        public List<CerealProduct> GetAllCereal()
        {
            var cereals = _dbContext.Cereals.ToList();

            return cereals;
        }

        public async Task<bool> PostCereal(CerealProduct cereal)
        {
            _dbContext.Add(cereal);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateCereal(
            CerealProduct oldCereal, CerealProduct newCereal)
        {
            _dbContext.Entry(oldCereal).CurrentValues.SetValues(newCereal);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteCereal(CerealProduct cereal)
        {
            _dbContext.Remove(cereal);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
