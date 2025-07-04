using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObjectPool : MonoBehaviour
{
    [SerializeField] private Cube _objectToPool;
    [SerializeField] private int _pooledAtStart = 100;

    private List<Cube> _pooledObjects = new();

    private void Start()
    {
        Cube buffer;

        for (int i  = 0; i < _pooledAtStart; i++)
        {
            buffer = Instantiate(_objectToPool);
            _pooledObjects.Add(buffer);
            buffer.gameObject.SetActive(false);
        }
    }

    public Cube GetCube()
    {
        foreach (Cube obj in _pooledObjects)
            if (obj.gameObject.activeInHierarchy == false)
                return obj;

        Cube buffer = Instantiate(_objectToPool);
        _pooledObjects.Add(buffer);
        return buffer;
    }
}
