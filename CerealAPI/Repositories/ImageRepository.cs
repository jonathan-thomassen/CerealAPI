using CerealAPI.Contexts;
using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private static readonly ImageContext _dbContext = new();

        public ImageEntry? GetImageEntryByCerealId(int cerealId)
        {
            var imageEntry = _dbContext.Images.First(i => i.CerealId == cerealId);

            return imageEntry;
        }

        public async Task<bool> PostImageEntry(ImageEntry entry)
        {
            _dbContext.Add(entry);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
