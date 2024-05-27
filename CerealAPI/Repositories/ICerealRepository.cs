using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public interface ICerealRepository
    {
        List<CerealProduct> GetAllCereal();
    }
}
