using Microsoft.EntityFrameworkCore;

using CerealAPI.Contexts;
using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private static readonly ImageContext s_dbContext = new();
        private static readonly object s_lock = new();

        public async Task<ImageEntry?> GetImageEntryById(int id)
        {
            ImageEntry? imageEntry =
                await s_dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);

            return imageEntry;
        }

        public async Task<ImageEntry?> GetImageEntryByCerealId(int cerealId)
        {
            ImageEntry? imageEntry = await s_dbContext.Images
                .FirstOrDefaultAsync(i => i.CerealId == cerealId);

            return imageEntry;
        }

        public async Task<bool> PostImageEntry(ImageEntry entry)
        {
            await s_dbContext.AddAsync(entry);
            int result = await s_dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateImageEntry(
            ImageEntry oldImageEntry, ImageEntry newImageEntry)
        {
            lock (s_lock)
            {
                s_dbContext.Entry(oldImageEntry).CurrentValues
                .SetValues(newImageEntry);
            }
            int result = await s_dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteImageEntry(ImageEntry entry)
        {
            lock (s_lock)
            {
                s_dbContext.Remove(entry);
            }
            int result = await s_dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
