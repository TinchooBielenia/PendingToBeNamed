using UnityEngine;
using UnityEngine.AI;

//TP2 - Juliana Dimeglio - Martin Bielenia
public class TankEnemy : Enemy
{
    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _activationRange = 10f;
    [SerializeField] private float _moveStopDistance = 2f;
    [SerializeField] private float _speed;
    private Animator _animator;
    private bool _playerInAttackRange = false;
    private bool _wasProvoked;
    public bool PlayerInAttackRange { set => _playerInAttackRange = value; }

    private void Start()
    {
        _enemyLife = _maxEnemyLife;
        _animator = GetComponentInChildren<Animator>();
        _agent.isStopped = true;
        _player = Player.Instance.transform;
        _agent.speed = _speed;
    }

    private void Update()
    {
        if (IsDead || _player == null) return;
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

    public override void TakeHit(DamageData data)
    {
        base.TakeHit(data);

        _wasProvoked = true;
        if (!IsDead && _animator != null)
        {
            if (data.Type == DamageType.Fire)
            {
                TakeDamage(data.Amount * base._trapDamageMultiplier);
            }
            else
            {
                TakeDamage(data.Amount);
            }

            _animator.SetTrigger("Hit");
        }
        if (IsDead)
        {
            if (_animator != null)
            {
                _animator.applyRootMotion = true;
                _animator.SetTrigger("Dead");
                _animator.SetBool("isAttacking", false);
            }
        }
    }

    public void DetectPlayerInAttackRange()
    {
        _agent.isStopped = true;
        RotateTowardsPlayer();
    }

    private void UpdateAnimationStates(float distance)
    {
        bool isInDetectionRange = distance <= _activationRange;

        AnimationsManager(isInDetectionRange || _wasProvoked, _playerInAttackRange);
    }

    private void AnimationsManager(bool playerInDetectionRange, bool playerInAttackRange)
    {
        if (IsDead) return;

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
}