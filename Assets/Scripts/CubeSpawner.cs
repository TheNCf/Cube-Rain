using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _cubeObjectPool;
    [SerializeField] private float _spawnHeightDelta = 10.0f;
    [SerializeField] private float _spawnDelay = 0.2f;

    public void AddPlatformForSpawn(Platform platform)
    {
        StartCoroutine(ProcessPlatform(platform));
    }

    public IEnumerator ProcessPlatform(Platform platform)
    {
        while (platform != null)
        {
            yield return new WaitForSeconds(_spawnDelay);

            GameObject cube = _cubeObjectPool.GetPooledObject();

            if (cube == null)
                continue;

            Vector3 platformPosition = platform.transform.position;
            float halfSizeX = platform.Size.x / 2;
            float halfSizeY = platform.Size.y / 2;
            float randomX = Random.Range(platformPosition.x - halfSizeX, platformPosition.x + halfSizeX);
            float randomY = Random.Range(platformPosition.y - halfSizeY, platformPosition.y + halfSizeY);
            Vector3 spawnPosition = new Vector3(randomX, _spawnHeightDelta + platformPosition.z, randomY);

            cube.transform.position = spawnPosition;
            cube.transform.rotation = Quaternion.identity;
            cube.SetActive(true);
        }
    }
}
