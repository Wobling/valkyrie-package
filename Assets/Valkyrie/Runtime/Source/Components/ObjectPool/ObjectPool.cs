using System.Collections.Generic;
using UnityEngine;

namespace Valkyrie.Components.ObjectPools
{
    public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab = null;
        [SerializeField] private int _poolSize = 0;
        
        private List<T> _availableItems;
        private List<T> _unavailableItems;

        private void Awake()
        {
            _availableItems = new List<T>(_poolSize);
            _unavailableItems = new List<T>(_poolSize);

            PopulatePool();
        }

        private void PopulatePool()
        {
            for (var i = 0; i < _poolSize; i++)
            {
                T pooledObject = Instantiate(_prefab, transform);
                pooledObject.gameObject.SetActive(false);
                _availableItems.Add(pooledObject);
            }
        }

        /// <summary>
        /// Returns an object from the pool. Returns null if there are no more objects free in the pool.
        /// </summary>
        /// <returns>Object of type T from the pool.</returns>
        public T Get()
        {
            int numFree = _availableItems.Count;
            if (numFree == 0)
                return null;

            // Pull an object from the end of the free list.
            T pooledObject = _availableItems[numFree - 1];
            _availableItems.RemoveAt(numFree - 1);
            _unavailableItems.Add(pooledObject);
            return pooledObject;
        }
        
        /// <summary>
        /// Returns an object to the pool. The object must have been created by this ObjectPool.
        /// </summary>
        /// <param name="pooledObject">Object previously obtained from this ObjectPool</param>
        public void ReturnObject(T pooledObject)
        {
            Debug.Assert(_unavailableItems.Contains(pooledObject));

            // Put the pooled object back in the free list.
            _unavailableItems.Remove(pooledObject);
            _availableItems.Add(pooledObject);

            // Re-parent the pooled object to us, and disable it.
            Transform pooledObjectTransform = pooledObject.transform;
            pooledObjectTransform.parent = transform;
            pooledObjectTransform.localPosition = Vector3.zero;
            pooledObject.gameObject.SetActive(false);
        }
    }
}

