using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public interface ICerealRepository
    {
        Task<CerealProduct?> GetCerealById(int id);
        List<CerealProduct> GetAllCereal();
        Task<CerealProduct?> PostCereal(CerealProduct cereal);
        Task<bool> DeleteCereal(CerealProduct cereal);
    }
}
