using SpaceWarsHex.Interfaces.Bridges;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.Bridges
{
    public class ConcurencyManager<TEntity> : IConcurencyManager<TEntity>
        where TEntity : notnull
    {

        private readonly Action<TEntity, Action> _start;
        private readonly Action<TEntity>? _onEntityComplete;
        private readonly Action _onComplete;
        private readonly ConcurrentDictionary<TEntity, byte> _pending;

        private void OnEntityDone(TEntity obj)
        {
            if (_pending.TryRemove(obj, out _))
            {
                _onEntityComplete?.Invoke(obj);
            }
            if (_pending.IsEmpty)
            {
                _onComplete?.Invoke();
            }
        }

        /// <summary>
        /// Create the Single-use helper.
        /// </summary>
        /// <param name="entities">The entities which have coroutines.</param>
        /// <param name="start">The action that will start the coroutine and call an action when done.</param>
        /// <param name="onEntityComplete">Action that will be called on each entity when they are done.</param>
        /// <param name="onAllComplete">Action that will be called when all coroutines are complete.</param>
        public ConcurencyManager(ICollection<TEntity> entities, Action<TEntity, Action> start, Action<TEntity> onEntityComplete, Action onAllComplete)
        {
            if (entities is null || entities.Count == 0)
            {
                onAllComplete();
                _start = (_, __) => { };
                _onComplete = () => { };
                _pending = new ConcurrentDictionary<TEntity, byte>();
            }
            else
            {
                _start = start ?? throw new ArgumentNullException(nameof(start));
                _onComplete = onAllComplete;
                _onEntityComplete = onEntityComplete;
                _pending = new ConcurrentDictionary<TEntity, byte>(entities.Select(e => new KeyValuePair<TEntity, byte>(e, 0)));
            }
        }

        public void Start()
        {
            foreach (var entity in _pending.Keys.ToList()) // copy in case there is some race condtion
            {
                _start(entity, () => OnEntityDone(entity));
            }
        }
    }
}
