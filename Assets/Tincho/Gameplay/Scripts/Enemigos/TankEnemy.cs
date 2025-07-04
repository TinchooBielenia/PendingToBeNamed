
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : Enemy, IDamageEnemy   
{
    public bool inRange;
    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AudioSource _getDamagedSFX;
    [SerializeField] private float _activationRange = 10f;
    [SerializeField] private float _moveStopDistance = 2f;
    private Animator _animator;
    private bool _playerInAttackRange = false;
    [SerializeField] private GameObject _damageArea;
    private bool _wasProvoked;

    public bool PlayerInAttackRange
    {
        get { return _playerInAttackRange; }
        set { _playerInAttackRange = value; }
    }

    private void Start()
    {
        _enemyLife = _maxEnemyLife;
        inRange = false;
        _agent.isStopped = true;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_isDead || _player == null) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        UpdateAttackRange(distance);
        HandleDetectionAndMovement(distance);
        UpdateAnimationStates(distance);
    }

    private void UpdateAttackRange(float distance)
    {
        _playerInAttackRange = distance <= _moveStopDistance;
    }

    private void HandleDetectionAndMovement(float distance)
    {
        bool isInDetectionRange = distance <= _activationRange;

        if ((isInDetectionRange || _wasProvoked) && !_playerInAttackRange)
        {
            _agent.isStopped = false;

            Vector3 direction = (_player.position - transform.position).normalized;
            Vector3 target = _player.position - direction * _moveStopDistance;
            _agent.SetDestination(target);
            RotateTowardsPlayer();
        }
        else
        {
            _agent.isStopped = true;
        }
    }

    private void UpdateAnimationStates(float distance)
    {
        bool isInDetectionRange = distance <= _activationRange;

        AnimationsManager(isInDetectionRange || _wasProvoked, _playerInAttackRange);
    }
    protected override void Death()
    {
        if (_isDead) return;

        if (_enemyLife <= 0)
        {
            _isDead = true;

            if (_animator != null)
            {
                _animator.applyRootMotion = false;
                _animator.SetTrigger("Dead");
                _animator.SetBool("isAttacking", false);
            }

            inRange = false;

            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
            LootOnDeath(); 

            Destroy(gameObject, 5f); 
        }
    }

    public void TakeHit(int damage)
    {
        _wasProvoked = true;
        GetDamage(damage);
        _getDamagedSFX.Play();
        if (!_isDead && _animator != null)
        {
            _animator.SetTrigger("Hit");
        }
        Death();
    }

    public void DetectPlayerInAttackRange()
    {
        if (!_isDead)
        {
            _agent.isStopped = true;
            RotateTowardsPlayer();
        }
    }

    private void AnimationsManager(bool playerInDetectionRange, bool playerInAttackRange)
    {
        if (_isDead) return;

        if (playerInDetectionRange && !playerInAttackRange)
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isAttacking", false);
        }
        else if (playerInDetectionRange && playerInAttackRange)
        {
            RotateTowardsPlayer();
            _animator.SetBool("isAttacking", true);
        }
        else if (!playerInDetectionRange && !playerInAttackRange)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isAttacking", false);
        }

    }
    public void EnableDamageCollider()
    {
        _damageArea.SetActive(true);
    }

    public void DisableDamageCollider()
    {
        _damageArea.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _activationRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _moveStopDistance); 
    }
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0f; 

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}