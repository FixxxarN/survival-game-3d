using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    [SerializeField] private float _mobHealth;
    private float _mobMaxHealth = 100f;

    [SerializeField] private float _radius;
    [SerializeField] private float _timer;
    [SerializeField] private float _idleTimer;

    private NavMeshAgent _agent;
    private Rigidbody _player;
    private Transform _target;

    private float _currentTime;
    private float _currentIdleTimer;
    private bool _idle;
    private float _distance;

    private float _playerDamage = 10f;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();

        _currentTime = _timer;
        _currentIdleTimer = _idleTimer;
    }
    void Start()
    {
        _mobHealth = _mobMaxHealth;

        _player = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        _currentIdleTimer += Time.deltaTime;

        if (_currentIdleTimer >= _idleTimer)
        {
            StartCoroutine("SwitchIdle");
        }

        if (_currentTime >= _timer && !_idle)
        {
            Vector3 _newPos = RandomNavSphere(transform.position, _radius, -1);

            _currentTime = 0;
            _agent.SetDestination(_newPos);
        }
    }

    IEnumerator SwitchIdle()
    {
        _idle = true;
        yield return new WaitForSeconds(5);

        _currentIdleTimer = 0;
        _idle = false;
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 _randomDirection = Random.insideUnitSphere * distance;
        _randomDirection += origin;

        NavMeshHit _navHit;
        NavMesh.SamplePosition(_randomDirection, out _navHit, distance, layermask);

        return _navHit.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mobHealth -= _playerDamage;
            }
            if (_mobHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _mobHealth = 100f;
        }
    }
}
