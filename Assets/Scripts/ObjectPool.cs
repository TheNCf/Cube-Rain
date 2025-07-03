using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _objectCount = 100;

    private List<GameObject> _pooledObjects = new List<GameObject>();

    private void Start()
    {
        GameObject buffer;

        for (int i  = 0; i < _objectCount; i++)
        {
            buffer = Instantiate(_objectToPool);
            _pooledObjects.Add(buffer);
            buffer.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in _pooledObjects)
            if (!obj.activeInHierarchy)
                return obj;

        return null;
    }
}
