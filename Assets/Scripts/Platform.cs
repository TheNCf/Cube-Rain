using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    public Vector2 Size { get; private set; }

    private void Awake()
    {
        float sizeX = transform.localScale.x * Mathf.Cos(transform.eulerAngles.z * Mathf.PI * 2.0f / 360.0f);
        float sizeZ = transform.localScale.z * Mathf.Cos(transform.eulerAngles.x * Mathf.PI * 2.0f / 360.0f);
        Size = new Vector2(sizeX, sizeZ);
    }

    private void Start()
    {
        _cubeSpawner.AddPlatformForSpawn(this);
    }
}
