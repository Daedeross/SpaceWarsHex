using SpaceWars.Interfaces;
using SpaceWars.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWars.Helpers
{
    /// <summary>
    /// Sparse Matrix to hold a collections of <see cref="IHexObject"/>s sorted by their hex position(s).
    /// </summary>
    public class HexMatrix<TEntity> : ICollection<TEntity>, IReadOnlyCollection<TEntity>, IDisposable
        where TEntity : IHexObject
    {
        // using a List<T> instead of a HashSet<T> since there will likely never be more that a few entities occupying the same hex at a time.
        private readonly IDictionary<HexVector2, List<TEntity>> _lookup;         // key = hex, value = all entities that occupy that hex.
        private readonly IDictionary<TEntity, ISet<HexVector2>> _reverseLookup;  // key = entity, value = all hexes the entity occupies.
        private bool disposedValue;

        /// <summary>
        /// Public constructor of <see cref="HexMatrix{TEntity}"/>
        /// </summary>
        public HexMatrix()
        {
            _lookup = new Dictionary<HexVector2, List<TEntity>>();
            _reverseLookup = new Dictionary<TEntity, ISet<HexVector2>>();
        }

        #region ICollection<T> implementation

        /// <inheritdoc />
        public int Count => _reverseLookup.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>
        /// Add a new entity to the collection.
        /// </summary>
        /// <param name="item">The entity to add.</param>
        /// <exception cref="ArgumentException">If <paramref name="item"/> already exists in the collection.</exception>
        public void Add(TEntity item)
        {
            HashSet<HexVector2> hexes;

            if (item is IMultiHexObject multi)
            {
                hexes = multi.GetHexes().ToHashSet();
            }
            else
            {
                hexes = new HashSet<HexVector2> { item.Position };
            }
            _reverseLookup.Add(item, hexes);

            foreach (var hex in hexes)
            {
                _lookup.AddOrAppend(hex, item);
            }

            if (item is INotifyPositionChanged npc)
            {
                npc.PositionChanged += OnPositionChanged;
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            _lookup.Clear();
            _reverseLookup.Clear();
        }

        /// <inheritdoc />
        public bool Contains(TEntity item)
        {
            return _reverseLookup.ContainsKey(item);
        }

        /// <inheritdoc />
        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            _reverseLookup.Keys.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public IEnumerator<TEntity> GetEnumerator()
        {
            return _reverseLookup.Keys.GetEnumerator();
        }

        /// <inheritdoc />
        public bool Remove(TEntity item)
        {
            if (_reverseLookup.TryGetValue(item, out var hexes))
            {
                foreach (var hex in hexes)
                {
                    _lookup.RemoveAndClean(hex, item);
                }

                _reverseLookup.Remove(item);
                if (item is IMovingHexObject mho)
                {
                    mho.PositionChanged -= OnPositionChanged;
                }
                return true;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _reverseLookup.Keys.GetEnumerator();
        }

        #endregion // ICollection<T> implementation

        /// <summary>
        /// Adds a new entity to the collection or, if it already exists, updates an entity's current hexes in the matrix.
        /// </summary>
        /// <param name="entity">The entity to add or update.</param>
        public void AddOrUpdate(TEntity entity)
        {
            // remove old indices
            Remove(entity);

            // (re)add with new indices.
            Add(entity);
        }

        /// <summary>
        /// Gets all entities that overlap with the provided hex.
        /// </summary>
        /// <param name="hex">The hex to check.</param>
        /// <returns>A collection of <see cref="IHexObject"/></returns>
        public ICollection<TEntity> GetCollidingEntities(HexVector2 hex)
        {
            // Call .ToList() to create a copy and ensure the caller does not mutate the internal collection stored in _lookup;
            return _lookup.TryGetValue(hex, out var collidingEntities) ? collidingEntities.ToList() : new List<TEntity>();
        }

        /// <summary>
        /// Gets all entities that overlap with the provided entity.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <returns>A collection of <see cref="IHexObject"/></returns>
        public ICollection<TEntity> GetCollidingEntities(IHexObject entity)
        {
            var results = new HashSet<TEntity>();

            if (entity is IMultiHexObject multi)
            {
                foreach (var hex in multi.GetHexes())
                {
                    if (_lookup.TryGetValue(hex, out var entities))
                    {
                        results.UnionWith(entities);
                    }
                }
            }
            else
            {
                if (_lookup.TryGetValue(entity.Position, out var entities))
                {
                    results.UnionWith(entities);
                }
            }

            return results;
        }

        internal void UpdateAllPositions()
        {
            var all = _reverseLookup.Keys.ToList();
            foreach (var entity in all)
            {
                AddOrUpdate(entity);
            }
        }

        private void OnPositionChanged(IHexObject entity, PositionChangedEventArgs e)
        {
            if (!_reverseLookup.ContainsKey((TEntity)entity))
            {
                throw new InvalidOperationException($"Unknown entity's PositionChanged subscribed to.");
            }
            if (entity is IMultiHexObject mho)
            {
                UpdateMultiHexObject(mho);
            }
            else
            {
                UpdateSingleHexObject(entity, e.OldPosition, e.NewPosition);
            }
        }

        private void UpdateSingleHexObject(IHexObject obj, HexVector2 oldPosition, HexVector2 newPosition)
        {
            if (oldPosition == newPosition)
            {
                return;
            }
            var entity = (TEntity)obj;
            _reverseLookup[entity] = new HashSet<HexVector2> { newPosition };

            _lookup[oldPosition].Remove(entity);
            _lookup.AddOrAppend(newPosition, entity);
        }

        private void UpdateMultiHexObject(IMultiHexObject obj)
        {
            var entity = (TEntity)obj;
            var newHexes = obj.GetHexes();

            var removed = _reverseLookup[entity];
            var added = newHexes.Except(removed);
            removed.ExceptWith(newHexes);

            _reverseLookup[entity] = new HashSet<HexVector2>(newHexes);
            foreach (var hex in removed)
            {
                _lookup[hex].Remove(entity);
            }
            foreach (var hex in added)
            {
                _lookup[hex].Add(entity);
            }
        }

        #region IDisposable

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var item in _reverseLookup.Keys)
                    {
                        if (item is INotifyPositionChanged npc)
                        {
                            npc.PositionChanged -= OnPositionChanged;
                        }
                    }
                    _reverseLookup.Clear();
                    _lookup.Clear();
                }

                disposedValue = true;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion // IDisposable
    }
}
