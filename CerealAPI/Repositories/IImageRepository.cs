using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public interface IImageRepository
    {
        ImageEntry? GetImageEntryByCerealId(int cerealId);
        Task<bool> PostImageEntry(ImageEntry entry);
    }
}
