using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : UnityEngine.Object
{
    private readonly List<T> _pooledObjectList;

    private int _pooledObjectsOnStart;

    private Func<T> _createFunction;

    public ObjectPool(Func<T> createFunction, int pooledObjectsOnStart)
    {
        _createFunction = createFunction;
        _pooledObjectsOnStart = pooledObjectsOnStart;
    }

    public T Get()
    {
        return _pooledObjectList[0];
    }

    public void Release(T obj)
    {

    }
}
