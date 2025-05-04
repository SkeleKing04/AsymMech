using System.Collections;
using UnityEngine;

public class SlightlySmarterEnemy : EnemyBase
{
    [SerializeField] private Vector2 _minMaxRandMoveTime;
    [SerializeField] private Vector2 _minMaxRandMoveDistance;
    private Vector3 _targetDestination;
    private float _destinationDistance;
    private float _moveTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        _moveTimer = Random.Range(_minMaxRandMoveTime.x, _minMaxRandMoveTime.y);
        base.Start();
        _targetDestination = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(_destinationDistance >= 0.1)
        {
            transform.position = Vector3.Lerp(transform.position, _targetDestination, 0.2f);
            _destinationDistance = Vector3.Distance(_targetDestination, transform.position);
        }
        else if(_moveTimer > 0)
        {
            _moveTimer -= Time.deltaTime;
            base.Update();
        }
        else if(_destinationDistance < 0.1)
        {
            float angle = Random.Range(0, 361) * Mathf.Deg2Rad;
            float distance = Random.Range(_minMaxRandMoveDistance.x, _minMaxRandMoveDistance.y);
            _targetDestination = transform.position + distance * (transform.up * Mathf.Cos(angle) + transform.right * Mathf.Sin(angle));
            _destinationDistance = Vector3.Distance(_targetDestination, transform.position);
            _moveTimer = Random.Range(_minMaxRandMoveTime.x, _minMaxRandMoveTime.y);
        }
    }
}
