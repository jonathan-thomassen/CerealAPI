using CerealAPI.Enums;
using CerealAPI.Models;

namespace CerealAPI.Services
{
    public interface IImageService
    {
        Task<(byte[]? image, ImageType? imageType)>GetImageByCerealId(
            int cerealId);
        Task<ImageEntry?> PostImage(int cerealId, IList<IFormFile> fileList);
        Task<(ImageEntry? imageEntry, bool existed)> UpdateImageEntry(
            ImageEntry newImageEntry);
        Task<bool?> DeleteImageByCerealId(int cerealId);
    }
}
