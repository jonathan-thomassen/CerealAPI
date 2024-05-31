using System.Collections.Concurrent;

using CerealAPI.Models;

namespace CerealAPI.Repositories
{
    public abstract class FakeDatabase<T> where T : Dto
    {
        private static readonly ConcurrentDictionary<int, T> _dictionary = new();
        private static int _currentId = 1;
        private readonly static object _idLock = new();

        protected int Insert(T item)
        {
            var id = GetNextId();
            _dictionary[id] = item with { Id = id };

            return id;
        }

        protected T? Get(int id) =>
            _dictionary.TryGetValue(id, out var value) ? value : null;

        protected IEnumerable<T> Get(Func<T, bool> pickItems) =>
            _dictionary.Values.Where(pickItems);

        protected void Update(T item, int id)
        {
            if (_dictionary.ContainsKey(id))
            {
                _dictionary[id] = item with { Id = id };
            }
            else
            {
                throw new SystemException($"Item with id {id} does not exists - unable to update.");
            }
        }

        protected void Delete(int id)
        {
            _dictionary.TryRemove(id, out var _);
        }

        private static int GetNextId()
        {
            int id;
            lock (_idLock)
            {
                id = _currentId++;
            }

            return id;
        }
    }
}
