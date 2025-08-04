using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : SpawnerBase<Bomb>
{
    [SerializeField] private SpawnerStatsView _spawnerStatsView;

    private int _spawnCount = 0;

    public void Spawn(Vector3 position)
    {
        Bomb bomb = ObjectPool.Get();
        _spawnCount++;
        _spawnerStatsView.Render(_spawnCount, ObjectPool.CountAll, ObjectPool.CountActive);
        bomb.transform.position = position;
    }

    protected override Bomb Create()
    {
        Bomb bomb = Instantiate(PrefabToSpawn, transform);
        bomb.gameObject.SetActive(false);
        return bomb;
    }

    protected override void OnGet(Bomb bomb)
    {
        bomb.Exploded += ObjectPool.Release;
        bomb.gameObject.SetActive(true);
        bomb.ResetObject();
    }

    protected override void OnRelease(Bomb bomb)
    {
        bomb.Exploded -= ObjectPool.Release;
        bomb.gameObject.SetActive(false);
    }

    protected override void OnClear(Bomb bomb)
    {
        Destroy(bomb.gameObject);
    }
}
