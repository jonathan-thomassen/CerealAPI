using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public interface IImageRepository
    {
        Task<ImageEntry?> GetImageEntryByCerealId(int cerealId);
        Task<bool> PostImageEntry(ImageEntry entry);
        Task<bool> DeleteImageEntry(ImageEntry entry);
    }
}
