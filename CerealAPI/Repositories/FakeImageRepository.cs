// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public class FakeImageRepository : FakeDatabase<ImageEntry>, IImageRepository
    {
        private static readonly object s_lock = new();

        public Task<ImageEntry?> GetImageEntryById(int id)
        {
            ImageEntry? imageEntry = Get(x => x.Id == id)
                .FirstOrDefault() ?? throw new SystemException(
                    $"Image entry does not exist with id {id}.");

            return Task.FromResult<ImageEntry?>(imageEntry);
        }

        public Task<ImageEntry?> GetImageEntryByCerealId(int cerealId)
        {
            ImageEntry? imageEntry = Get(x => x.CerealId == cerealId)
                .FirstOrDefault() ?? throw new SystemException(
                    "Image entry does not exist with " +
                    $"associated cereal id {cerealId}.");

            return Task.FromResult<ImageEntry?>(imageEntry);
        }

        public Task<bool> PostImageEntry(ImageEntry imageEntry)
        {
            lock (s_lock)
            {
                if (Get(x => x.Id == imageEntry.Id).Any())
                    throw new SystemException(
                        $"Image entry already added for {imageEntry.Id}.");

                int id = Insert(imageEntry);
                return Task.FromResult(id > 0);
            }
        }

        public Task<bool> UpdateImageEntry(
            ImageEntry oldImageEntry, ImageEntry newImageEntry)
        {
            lock (s_lock)
            {
                if (!Get(x => x.Id == oldImageEntry.Id).Any())
                    throw new SystemException(
                        $"No image entry exists with id {oldImageEntry.Id}.");

                Update(oldImageEntry, oldImageEntry.Id);

                return Task.FromResult(oldImageEntry.Id > 0);
            }
        }

        public Task<bool> DeleteImageEntry(ImageEntry entry)
        {
            Delete(entry.Id);
            return Task.FromResult(true);
        }
    }
}
