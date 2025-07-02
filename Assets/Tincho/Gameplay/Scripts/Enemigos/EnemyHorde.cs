using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHorde : Enemy
{
    [SerializeField] private float _stunDuration = 5f;

    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;
    private bool _isStunned = false;
    private float _stunTimer = 0f;

    [SerializeField] private Animator _animator;
    [SerializeField] private int _damageAmount;
    [SerializeField] private float _damageCooldown = 1f;
    private float _lastDamageTime = -Mathf.Infinity;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_isDead) return;
        if (_isStunned)
        {
            StunEnemy();
            return;
        }

        if (_player != null)
        {
            MoveEnemy();
        }
    }


    private void MoveEnemy()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        bool shouldMove = distance > 2f;

        _animator.SetBool("isRunning", shouldMove);

        if (shouldMove)
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            Vector3 targetPosition = _player.position - direction * 1.5f;

            _agent.SetDestination(targetPosition);
        }
        else
        {
            _agent.ResetPath();
        }
    }


    private void StunEnemy()
    {
        _stunTimer -= Time.deltaTime;

        if (_stunTimer <= 0f)
        {
            _isStunned = false;
            _agent.isStopped = false;
            if (_player != null)
            {
                _agent.SetDestination(_player.position);
                _animator.SetBool("isRunning", true);
            }
            return;
        }

        _agent.isStopped = true;

        if (_animator != null)
            _animator.SetBool("isRunning", false);
    }

    public override void TakeHit(int damage)
    {
        base.TakeHit(damage);
        _isStunned = true;
        _stunTimer = _stunDuration;

        if (_animator != null)
        {
            _animator.SetTrigger("Hit");
            _animator.SetBool("isRunning", false);
        }


        if (_isDead)
        {
            _animator.SetTrigger("Die");
            _agent.isStopped = true;
            LootOnDeath();
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
            Destroy(gameObject, 5f);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= _lastDamageTime + _damageCooldown)
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(_damageAmount);
                _lastDamageTime = Time.time;
            }
        }
    }
}
