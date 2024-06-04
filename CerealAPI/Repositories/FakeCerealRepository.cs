// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public class FakeCerealRepository : FakeDatabase<CerealProduct>, ICerealRepository
    {
        private static readonly object _lock = new();

        public Task<bool> PostCereal(CerealProduct cereal)
        {
            lock (_lock)
            {
                if (Get(x => x.Id == cereal.Id).Any())
                    throw new SystemException(
                        $"Cereal Product already added for {cereal.Id}.");

                int id = Insert(cereal);
                return Task.FromResult(id > 0);
            }
        }

        public Task<bool> DeleteCereal(CerealProduct cereal)
        {
            Delete(cereal.Id);
            return Task.FromResult(true);
        }

        public List<CerealProduct> GetAllCereal()
        {
            List<CerealProduct> cereals = GetAllCereal();

            return cereals;
        }

        public Task<CerealProduct?> GetCerealById(int id)
        {
            CerealProduct? cereal = Get(x => x.Id == id)
                .FirstOrDefault() ?? throw new SystemException(
                    $"Cereal does not exists with id {id}.");

            return Task.FromResult(cereal);
        }

        public Task<bool> UpdateCereal(CerealProduct oldCereal, CerealProduct newCereal)
        {
            lock (_lock)
            {
                if (Get(x => x.Id == oldCereal.Id).Count() == 0)
                    throw new SystemException($"No cereal exists with id {oldCereal.Id}.");

                Update(oldCereal, oldCereal.Id);

                return Task.FromResult(oldCereal.Id > 0);
            }
        }
    }
}
