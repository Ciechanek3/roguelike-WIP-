using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ObjectPooling
{
    public class ObjectPool<T> where T : MonoBehaviour, IPool
    {
        public List<T> ObjectsInPool { get; private set; }

        private readonly List<T> _initialItems;

        public ObjectPool(List<T> objectsToPool, int poolMultiplier)
        {
            ObjectsInPool = new List<T>();
            _initialItems = objectsToPool;
            MakePool(poolMultiplier);
        }

        public ObjectPool(T objectInPool, int poolMultiplier)
        {
            ObjectsInPool = new List<T>();
            _initialItems = new List<T>();
            _initialItems.Add(objectInPool);
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

        public T TakeElementFromPool([CanBeNull] Func<List<T>> filter = null)
        {
            T element;
            if (filter != null)
            {
                List<T> elements = filter.Invoke();
                if (elements.Count == 0)
                {
                    MakePool(1);
                    element = TakeElementFromPool(filter);
                }
                else
                {
                    element = elements[Random.Range(0, elements.Count)];
                }
            }
            else
            { 
                if (ObjectsInPool.Count == 0)
                {
                    MakePool(1);
                    element = TakeElementFromPool();
                }
                else
                {
                    element = ObjectsInPool[Random.Range(0, ObjectsInPool.Count)];  
                }
            }
            
            if (ObjectsInPool.Contains(element))
            {
                ObjectsInPool.Remove(element); 
            }

            element.gameObject.SetActive(true);
            return element;
        }

        public void GetElementToPool(T element)
        {
            element.gameObject.SetActive(false);
            ObjectsInPool.Add(element);
        }
    
    }
}