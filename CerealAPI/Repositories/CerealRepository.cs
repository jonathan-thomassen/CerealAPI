using CerealAPI.Models;
using CerealAPI.Contexts;
using System.Linq;

namespace CerealAPI.Repositories
{
    public class CerealRepository : ICerealRepository
    {
        private static readonly CerealContext _dbContext = new CerealContext();

        public List<CerealProduct> GetAllCereal()
        {
            var cereals = _dbContext.Set<CerealProduct>().ToList();

            return cereals;
        }
    }
}
