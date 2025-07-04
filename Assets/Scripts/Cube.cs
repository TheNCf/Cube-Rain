using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Material _material;
    private Rigidbody _rigidbody;

    private ColorService _colorService;

    private Color _standartColor;
    private bool _isTouchedPlatform = false;
    private float _lifespan;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _rigidbody = GetComponent<Rigidbody>();

        _colorService = new ColorService();

        _standartColor = _material.color;
    }

    private void OnEnable()
    {
        _isTouchedPlatform = false;
        _material.color = _standartColor;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouchedPlatform == false && collision.collider.TryGetComponent(out Platform _))
        {
            _material.color = _colorService.PickRandom();
            _isTouchedPlatform = true;
            _lifespan = Random.Range(2.0f, 5.0f);
            StartCoroutine(SetDisabled(_lifespan));
        }
    }

    private IEnumerator SetDisabled(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
