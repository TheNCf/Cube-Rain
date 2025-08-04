using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerStatsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _spawnedOverallText;
    [SerializeField] private TextMeshProUGUI _objectsInPoolText;
    [SerializeField] private TextMeshProUGUI _objectsActiveText;
    [Space(10)]
    [SerializeField] private string _spawnedOverallPrefix = "Spawns: ";
    [SerializeField] private string _objectsInPoolPrefix = "In Pool: ";
    [SerializeField] private string _objectsActivePrefix = "Active: ";

    public void Render(int spawnedOverall, int objectsInPool, int objectsActive)
    {
        _spawnedOverallText.text = _spawnedOverallPrefix + spawnedOverall.ToString();
        _objectsInPoolText.text = _objectsInPoolPrefix + objectsInPool.ToString();
        _objectsActiveText.text = _objectsActivePrefix + objectsActive.ToString();
    }
}
