using System.Collections.Concurrent;

using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public abstract class FakeDatabase<T> where T : Dto
    {
        private static readonly ConcurrentDictionary<int, T> s_dictionary = new();
        private static int s_currentId = 1;
        private static readonly object s_idLock = new();

        protected int Insert(T item)
        {
            int id = GetNextId();
            s_dictionary[id] = item with { Id = id };

            return id;
        }

        protected T? Get(int id) =>
            s_dictionary.TryGetValue(id, out T? value) ? value : null;

        protected IEnumerable<T> Get(Func<T, bool> pickItems) =>
            s_dictionary.Values.Where(pickItems);

        protected void Update(T item, int id)
        {
            if (s_dictionary.ContainsKey(id))
            {
                s_dictionary[id] = item with { Id = id };
            }
            else
            {
                throw new SystemException($"Item with id {id} does not exists - unable to update.");
            }
        }

        protected void Delete(int id)
        {
            s_dictionary.TryRemove(id, out T? _);
        }

        private static int GetNextId()
        {
            int id;
            lock (s_idLock)
            {
                id = s_currentId++;
            }

            return id;
        }
    }
}
