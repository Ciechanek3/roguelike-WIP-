using System;
using System.Collections.Generic;

public class ObjectPooler<T>
{
    public readonly List<T> ObjectsInPool;

    public ObjectPooler(List<T> objectsInPool)
    {
        ObjectsInPool = objectsInPool;
    }

    public T RemoveElementFromPool(Func<T> GetElement)
    {
        T element = GetElement.Invoke();
        ObjectsInPool.Remove(element);
        return element;
    }

    public void GetElementBackToPool(T element, Action<T> OnElementReturn)
    {
        ObjectsInPool.Add(element);
        OnElementReturn(element);
    }
}