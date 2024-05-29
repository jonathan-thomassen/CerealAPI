using CerealAPI.Models;
using CerealAPI.Contexts;

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
            var cereals = _dbContext.Set<CerealProduct>().ToList();

            return cereals;
        }

        public async Task<CerealProduct?> PostCereal(CerealProduct cereal)
        {
            _dbContext.Add(cereal);
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return cereal;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteCereal(CerealProduct cereal)
        {
            _dbContext.Remove(cereal);
            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
