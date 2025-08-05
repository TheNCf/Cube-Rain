using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : SpawnerBase<Bomb>
{
    public void Spawn(Vector3 position)
    {
        Bomb bomb = ObjectPool.Get();
        SpawnCount++;
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
