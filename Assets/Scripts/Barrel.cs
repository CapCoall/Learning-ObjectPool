using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class Barrel : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleEffect;
    [SerializeField] private int _pointValue;
    private GameManager _gameManager;
    private ObjectPool<Barrel> _pool;

    private void Awake()
    {
        _gameManager =GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnMouseDown()
    {
        _pool.Release(this);
        _gameManager.UpdateScore(_pointValue);
        Explode();
    }

    public void SetPool(ObjectPool<Barrel> pool)
    {
        _pool=pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        _pool.Release(this);
        if (gameObject.CompareTag("Bomb"))
        {
            _gameManager.GameOver();
        }
    }
    private void Explode()
    {
        Instantiate(_particleEffect,transform.position,_particleEffect.transform.rotation);
    }
}
