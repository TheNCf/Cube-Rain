using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
public class Bomb : MonoBehaviour, IPoolableObject
{
    [SerializeField, Min(0.0001f)] private float _radius = 5.0f;
    [SerializeField] private float _explosionForce = 10.0f;
    [SerializeField] private float _upwardsModifier = 3.0f;

    [SerializeField, Min(0.0f)] private float _minLifetime = 2.0f;
    [SerializeField, Min(0.0f)] private float _maxLifetime = 5.0f;

    [SerializeField] private LayerMask _affectedLayers;

    private Rigidbody _rigidbody;
    private ColorChanger _colorChanger;

    private float _lifetime;

    public Action<Bomb> Exploded;

    private void OnValidate()
    {
        if (_maxLifetime < _minLifetime)
            _minLifetime = _maxLifetime;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Explode()
    {
        Collider[] objectsInRadius = Physics.OverlapSphere(transform.position, _radius, _affectedLayers);

        foreach (Collider obj in objectsInRadius)
            if (obj.TryGetComponent(out Rigidbody rigidbody))
                if (rigidbody != _rigidbody)
                    rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius, _upwardsModifier, ForceMode.Impulse);
    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Explode();
        Exploded?.Invoke(this);
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < _lifetime)
        {
            float alpha = 1 - elapsedTime / _lifetime;
            _colorChanger.SetAlpha(alpha);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }

    public void ResetObject()
    {
        _colorChanger.Default();
        StopAllCoroutines();
        _rigidbody.velocity = Vector3.zero;
        _lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
        StartCoroutine(ExplodeCoroutine());
        StartCoroutine(FadeOutCoroutine());
    }
}
