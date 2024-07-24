using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    private float _xRange = 5;
    private float _ySpawnPos = -2;
    private float _minSpeed = 12;
    private float _maxSpeed = 14;
    private float _maxTorque = 2;
    private float _minTorque = -2;

    private Rigidbody _barrelRb;

    [SerializeField] private Barrel[] barrelPrefab;
    public ObjectPool<Barrel> _pool;

    private void Start()
    {
        _pool = new ObjectPool<Barrel>(CreateBarrel, OnTakeBarrelFromPool, OnReturnBarrelToPool, OnDestroyBarrel, false, 20, 100);
    }

    private Barrel CreateBarrel()
    {
        int index=Random.Range(0, barrelPrefab.Length);
        Barrel barrel = Instantiate(barrelPrefab[index]);
        barrel.gameObject.SetActive(false);
        barrel.SetPool(_pool);
        return barrel;
    }

    private void OnTakeBarrelFromPool(Barrel barrel)
    {
        _barrelRb = barrel.GetComponent<Rigidbody>();

        barrel.transform.position = RandomSpawnPosition();
        barrel.gameObject.SetActive(true);

        _barrelRb.AddForce(RandomForce(), ForceMode.Impulse);
        _barrelRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    private void OnReturnBarrelToPool(Barrel barrel)
    {
        barrel.gameObject.SetActive(false);
    }

    private void OnDestroyBarrel(Barrel barrel)
    {
        Destroy(barrel.gameObject);
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(_minTorque, _maxTorque);
    }
}
