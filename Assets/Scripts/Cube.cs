using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Material _material;
    private Rigidbody _rigidbody;

    private Color _standartColor;
    private bool _isColorChangeTriggered = false;
    private float _lifespan;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _rigidbody = GetComponent<Rigidbody>();

        _standartColor = _material.color;
    }

    private void OnEnable()
    {
        _isColorChangeTriggered = false;
        _material.color = _standartColor;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorChangeTriggered == false && collision.collider.TryGetComponent(out Platform _))
        {
            ColorChanger.RandomColor(_material);
            _isColorChangeTriggered = true;
            _lifespan = Random.Range(2.0f, 5.0f);
            Invoke(nameof(SetDisabled), _lifespan);
        }
    }

    private void SetDisabled()
    {
        gameObject.SetActive(false);
    }
}
