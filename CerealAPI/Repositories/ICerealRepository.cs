using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public interface ICerealRepository
    {
        Task<CerealProduct?> GetCerealById(int id);
        List<CerealProduct> GetAllCereal();
        Task<bool> PostCereal(CerealProduct cereal);
        Task<bool> UpdateCereal(
            CerealProduct oldCereal, CerealProduct newCereal);
        Task<bool> DeleteCereal(CerealProduct cereal);
    }
}
