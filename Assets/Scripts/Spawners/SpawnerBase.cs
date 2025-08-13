using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour where T : UnityEngine.Object, IPoolableObject
{
    [SerializeField] protected T PrefabToSpawn;
    [SerializeField] protected int PoolSizeAtStart = 100;

    protected ObjectPool<T> ObjectPool;
    private int _spawnCount = 0;

    public event Action<int> Spawned;

    public int SpawnCount 
    {
        get
        {
            return _spawnCount;
        } 

        protected set
        {
            _spawnCount = value;
            Spawned?.Invoke(value);
        }
    }
    public int InsatncesInPool => ObjectPool.CountAll;
    public int ActiveInPool => ObjectPool.CountActive;

    protected virtual void Awake()
    {
        ObjectPool = new ObjectPool<T>(Create, OnGet, OnRelease, OnClear, PoolSizeAtStart);
    }

    protected abstract T Create();

    protected abstract void OnGet(T obj);

    protected abstract void OnRelease(T obj);

    protected abstract void OnClear(T obj);
}
