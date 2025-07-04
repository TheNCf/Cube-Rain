using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    private Material _material;
    private Color _standartColor;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _standartColor = _material.color;
    }

    private void OnEnable()
    {
        _material.color = _standartColor;
    }

    public void PickRandom()
    {
        _material.color = Random.ColorHSV();
    }
}
