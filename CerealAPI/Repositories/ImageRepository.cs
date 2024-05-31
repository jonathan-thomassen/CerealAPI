using Microsoft.EntityFrameworkCore;

using CerealAPI.Contexts;
using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private static readonly ImageContext _dbContext = new();

        public async Task<ImageEntry?> GetImageEntryById(int id)
        {
            var imageEntry = await _dbContext.Images.FirstOrDefaultAsync(
                i => i.Id == id);

            return imageEntry;
        }

        public async Task<ImageEntry?> GetImageEntryByCerealId(int cerealId)
        {
            var imageEntry = await  _dbContext.Images.FirstOrDefaultAsync(
                i => i.CerealId == cerealId);

            return imageEntry;
        }

        public async Task<bool> PostImageEntry(ImageEntry entry)
        {
            _dbContext.Add(entry);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateImageEntry(
            ImageEntry oldImageEntry, ImageEntry newImageEntry)
        {
            _dbContext.Entry(oldImageEntry).CurrentValues
                .SetValues(newImageEntry);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteImageEntry(ImageEntry entry)
        {
            _dbContext.Remove(entry);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
