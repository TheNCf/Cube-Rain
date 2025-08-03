using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField, Min(0.0001f)] private float _radius = 5.0f;
    [SerializeField] private float _explosionForce = 10.0f;
    [SerializeField] private float _upwardsModifier = 3.0f;

    [SerializeField, Min(0.0f)] private float _minLifetime = 2.0f;
    [SerializeField, Min(0.0f)] private float _maxLifetime = 5.0f;

    [SerializeField] private LayerMask _affectedLayers;

    private Rigidbody _rigidbody;

    private void OnValidate()
    {
        if (_maxLifetime < _minLifetime)
            _minLifetime = _maxLifetime;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(ExplodeCoroutine());
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
        float lifetime = Random.Range(_minLifetime, _maxLifetime);
        yield return new WaitForSeconds(lifetime);
        Explode();
        //TODO: Destroy or release to object pool
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
