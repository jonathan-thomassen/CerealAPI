using CerealAPI.Models;
using CerealAPI.Contexts;

namespace CerealAPI.Repositories
{
    public class CerealRepository : ICerealRepository
    {
        private static readonly CerealContext _dbContext = new();

        public List<CerealProduct> GetAllCereal()
        {
            var cereals = _dbContext.Set<CerealProduct>().ToList();

            return cereals;
        }

        public async Task<CerealProduct?> PostCereal(CerealProduct cereal)
        {
            _dbContext.Add(cereal);
            var result = await _dbContext.SaveChangesAsync();

            if (result == 1)
            {
                return cereal;
            }
            else
            {
                return null;
            }
        }
    }
}
