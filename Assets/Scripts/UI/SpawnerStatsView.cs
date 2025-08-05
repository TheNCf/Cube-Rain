using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerStatsView<T> : MonoBehaviour where T : Object, IPoolableObject
{
    [SerializeField] private SpawnerBase<T> _spawner;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _spawnedOverallText;
    [SerializeField] private TextMeshProUGUI _objectsInPoolText;
    [SerializeField] private TextMeshProUGUI _objectsActiveText;
    [Space(10)]
    [SerializeField] private string _spawnedOverallPrefix = "Spawns: ";
    [SerializeField] private string _objectsInPoolPrefix = "In Pool: ";
    [SerializeField] private string _objectsActivePrefix = "Active: ";

    protected virtual void Awake()
    {
        _spawner.Spawned += Render;
    }

    public void Render(int spawnedOverall)
    {
        _spawnedOverallText.text = _spawnedOverallPrefix + spawnedOverall.ToString();
        _objectsInPoolText.text = _objectsInPoolPrefix + _spawner.InsatncesInPool;
        _objectsActiveText.text = _objectsActivePrefix + _spawner.ActiveInPool;
    }
}
