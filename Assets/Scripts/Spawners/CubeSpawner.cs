using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : SpawnerBase<Cube>
{
    [SerializeField] private List<Platform> _platformsForSpawn = new();
    [SerializeField] private float _spawnHeightDelta = 10.0f;
    [SerializeField] private float _spawnDelay = 0.2f;

    [SerializeField] private BombSpawner _bombSpawner;

    private void Start()
    {
        foreach (Platform platform in _platformsForSpawn)
        {
            StartCoroutine(ProcessPlatform(platform));
        }
    }

    private IEnumerator ProcessPlatform(Platform platform)
    {
        WaitForSeconds spawnWait = new WaitForSeconds(_spawnDelay);

        while (platform != null)
        {
            yield return spawnWait;

            Cube cube = ObjectPool.Get();
            SpawnCount++;

            Vector3 platformPosition = platform.transform.position;
            float halfSizeX = platform.Size.x / 2;
            float halfSizeY = platform.Size.y / 2;
            float randomX = Random.Range(platformPosition.x - halfSizeX, platformPosition.x + halfSizeX);
            float randomY = Random.Range(platformPosition.z - halfSizeY, platformPosition.z + halfSizeY);
            Vector3 spawnPosition = new Vector3(randomX, _spawnHeightDelta + platformPosition.y, randomY);

            cube.transform.position = spawnPosition;
            cube.transform.rotation = Quaternion.identity;
        }
    }

    protected override Cube Create()
    {
        Cube cube = Instantiate(PrefabToSpawn, transform);
        cube.gameObject.SetActive(false);
        return cube;
    }

    protected override void OnGet(Cube cube)
    {
        cube.LifespanExpired += OnCubeLifespanExpired;
        cube.gameObject.SetActive(true);
        cube.ResetObject();
    }

    protected override void OnRelease(Cube cube)
    {
        cube.LifespanExpired -= OnCubeLifespanExpired;
        cube.gameObject.SetActive(false);
    }

    protected override void OnClear(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    private void OnCubeLifespanExpired(Cube cube)
    {
        _bombSpawner.Spawn(cube.transform.position);
        ObjectPool.Release(cube);
    }
}
