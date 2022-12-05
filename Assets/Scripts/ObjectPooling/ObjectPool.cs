using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPooling
{
    public class ObjectPool<T> where T : MonoBehaviour, IPool
    {
        public List<T> ObjectsInPool { get; }

        private readonly List<T> _initialItems;

        public ObjectPool(List<T> objectsToPool, int poolMultiplier)
        {
            ObjectsInPool = new List<T>();
            _initialItems = objectsToPool;
            MakePool(poolMultiplier);
        }

        public void MakePool(int poolMultiplier)
        {
            for (int i = 0; i < poolMultiplier; i++)
            {
                for (int j = 0; j < _initialItems.Count; j++)
                {
                    T t = Object.Instantiate<T>(_initialItems[j]);
                    t.gameObject.SetActive(false);
                    ObjectsInPool.Add(t);
                }
            }
        }

        public T TakeElementFromPool(T element)
        {
            if (ObjectsInPool.Contains(element))
            {
                ObjectsInPool.Remove(element); 
            }
            element.Setup();
            return element;
        }

        public void GetElementToPool(T element)
        {
            element.gameObject.SetActive(false);
            ObjectsInPool.Add(element);
        }
    
    }
}