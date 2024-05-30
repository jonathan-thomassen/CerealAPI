using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public interface IImageRepository
    {
        Task<ImageEntry?> GetImageEntryById(int id);
        Task<ImageEntry?> GetImageEntryByCerealId(int cerealId);
        Task<bool> PostImageEntry(ImageEntry entry);
        Task<bool> UpdateImageEntry(
            ImageEntry oldImageEntry, ImageEntry newImageEntry);
        Task<bool> DeleteImageEntry(ImageEntry entry);
    }
}
