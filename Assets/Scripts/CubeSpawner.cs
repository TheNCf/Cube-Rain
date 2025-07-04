using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeObjectPool _cubeObjectPool;
    [SerializeField] private List<Platform> _platformsForSpawn = new();
    [SerializeField] private float _spawnHeightDelta = 10.0f;
    [SerializeField] private float _spawnDelay = 0.2f;

    private IEnumerator ProcessPlatform(Platform platform)
    {
        while (platform != null)
        {
            yield return new WaitForSeconds(_spawnDelay);

            Cube cube = _cubeObjectPool.GetCube();

            if (cube == null)
                continue;

            Vector3 platformPosition = platform.transform.position;
            float halfSizeX = platform.Size.x / 2;
            float halfSizeY = platform.Size.y / 2;
            float randomX = Random.Range(platformPosition.x - halfSizeX, platformPosition.x + halfSizeX);
            float randomY = Random.Range(platformPosition.z - halfSizeY, platformPosition.z + halfSizeY);
            Vector3 spawnPosition = new Vector3(randomX, _spawnHeightDelta + platformPosition.y, randomY);

            cube.transform.position = spawnPosition;
            cube.transform.rotation = Quaternion.identity;
            cube.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        foreach (Platform platform in _platformsForSpawn)
        {
            StartCoroutine(ProcessPlatform(platform));
        }
    }
}
