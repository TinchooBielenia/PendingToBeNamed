
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankEnemy : Enemy, IDamageEnemy   
{
    public bool inRange;
    [SerializeField] private Transform _player;
    [SerializeField] private int _speed;
    [SerializeField] private int _maxSpeed;
    [SerializeField] private Rigidbody _enemyRb;
    [SerializeField] private AudioSource _getDamagedSFX;
    private Animator _animator;
    private bool _playerInAttackRange = false;
    [SerializeField] private GameObject _damageArea;
    [SerializeField] private List<GameObject> _lootList;

    public bool PlayerInAttackRange
    {
        get { return _playerInAttackRange; }
        set { _playerInAttackRange = value; }
    }

    private void Start()
    {
        _enemyLife = _maxEnemyLife;
        inRange = false;
        _speed = _maxSpeed;
        _animator = GetComponentInChildren<Animator>();
        _enemyRb.isKinematic = false;
    }

    private void Update()
    {
        if (inRange && _player != null && !_playerInAttackRange)
        {
            DashAndStop(_player, _speed);
        }

        if (_player != null && _playerInAttackRange)
        {
            DetectPlayerInAttackRange();
        }

        AnimationsManager(inRange,_playerInAttackRange);

        if (_isDead)
        {
            if (_animator != null)
            {
                _animator.applyRootMotion = _isDead;
                _animator.SetBool("isDead", true);
                _animator.SetBool("isAttacking", false);
                inRange = false;
                Collider[] colliders = GetComponentsInChildren<Collider>();
                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }
                _enemyRb.isKinematic = true;

                LootOnDeath(_lootList);
            }
        }
    }

    public void TakeHit(int damage)
    {
        GetDamage(damage);
        _getDamagedSFX.Play();
        if (!_isDead && _animator != null)
        {
            _animator.SetTrigger("Hit"); 
        }
        Death();
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("El jugador entró en la zona del enemigo.");
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("El jugador salió en la zona del enemigo.");
            inRange = false;
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
        if (!_isDead)
        {
            _enemyRb.velocity = Vector3.zero;
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
            //_animator.SetBool("isRunning", false);
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