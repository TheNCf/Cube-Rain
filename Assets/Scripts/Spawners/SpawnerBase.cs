using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour where T : Object, IPoolableObject
{
    [SerializeField] protected T PrefabToSpawn;
    [SerializeField] protected int PoolSizeAtStart = 100;

    protected ObjectPool<T> ObjectPool;

    protected virtual void Awake()
    {
        ObjectPool = new ObjectPool<T>(Create, OnGet, OnRelease, OnClear, PoolSizeAtStart);
    }

    protected abstract T Create();

    protected abstract void OnGet(T cube);

    protected abstract void OnRelease(T cube);

    protected abstract void OnClear(T cube);
}
