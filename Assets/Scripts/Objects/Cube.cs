using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour, IPoolableObject
{
    [SerializeField] private float _terminationHeight = -10.0f;

    private Rigidbody _rigidbody;
    private ColorChanger _colorChanger;

    private bool _isTouchedPlatform = false;
    private float _lifespan;
    private float _minLifespan = 2.0f;
    private float _maxLifespan = 5.0f;

    public Action<Cube> LifespanExpired;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < _terminationHeight)
            LifespanExpired?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouchedPlatform == false && collision.collider.TryGetComponent(out Platform _))
        {
            _colorChanger.PickRandom();
            _isTouchedPlatform = true;
            _lifespan = UnityEngine.Random.Range(_minLifespan, _maxLifespan);
            StartCoroutine(LifespanCoroutine(_lifespan));
        }
    }

    private IEnumerator LifespanCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        LifespanExpired?.Invoke(this);
    }

    public void ResetObject()
    {
        StopAllCoroutines();
        _isTouchedPlatform = false;
        _rigidbody.velocity = Vector3.zero;
        _colorChanger.Default();
    }
}
