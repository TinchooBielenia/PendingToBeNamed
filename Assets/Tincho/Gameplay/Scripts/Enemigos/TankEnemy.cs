
using System.Collections.Generic;
using UnityEngine;

//TP2 - Juliana Dimeglio - Martin Bielenia
public class TankEnemy : Enemy  
{
    private bool _inRange;
    [SerializeField] private Transform _player;
    [SerializeField] private int _speed;
    [SerializeField] private int _maxSpeed;
    [SerializeField] private Rigidbody _enemyRb;
    private Animator _animator;
    private bool _playerInAttackRange = false;
    [SerializeField] private GameObject _damageArea;

    public bool PlayerInAttackRange
    {
        get { return _playerInAttackRange; }
        set { _playerInAttackRange = value; }
    }

    private void Start()
    {
        _enemyLife = _maxEnemyLife;
        _inRange = false;
        _speed = _maxSpeed;
        _animator = GetComponentInChildren<Animator>();
        _enemyRb.isKinematic = false;

    }

    private void Update()
    {
        if (IsDead) return; 

        if (_inRange && _player != null && !_playerInAttackRange)
        {
            DashAndStop(_player, _speed);
        }

        if (_player != null && _playerInAttackRange)
        {
            DetectPlayerInAttackRange();
        }

        AnimationsManager(_inRange, _playerInAttackRange);
    }

    public override void TakeHit(int damage)
    {
        base.TakeHit(damage);
        if (!IsDead && _animator != null)
        {
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

            _inRange = false;

            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }

            _enemyRb.isKinematic = true;

            LootOnDeath();

            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Debug.Log("El jugador entró en la zona del enemigo.");
            _inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Debug.Log("El jugador salió en la zona del enemigo.");
            _inRange = false;
        }
    }

    private void DashAndStop(Transform player, int speed)
    {
        //Basic movement
        Vector3 dir = (player.position - _transform.position).normalized;

        Vector3 currentVelocity = _enemyRb.velocity;
        _enemyRb.velocity = new Vector3(dir.x * speed, currentVelocity.y, dir.z * speed);

        // Rotación hacia el jugador
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

    }

    public void DetectPlayerInAttackRange()
    {
        if (!IsDead)
        {
            _enemyRb.velocity = Vector3.zero;
        }
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
}