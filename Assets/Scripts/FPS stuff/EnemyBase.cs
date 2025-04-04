using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    protected Transform _targetLocation;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _stopRange;
    [SerializeField] private ParticleSystem _deathParticles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _targetLocation = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Vector3.Distance(_targetLocation.position, transform.position) > _stopRange)
        {
            transform.LookAt(_targetLocation);
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }
    }

    protected virtual void OnDestroy()
    {
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
    }
}
