using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BombEnemy : Enemy, IDamageEnemy
{
    [SerializeField] private float _activationRange = 10f;
    [SerializeField] private float _explosionRange = 2f;
    [SerializeField] private int _explosionDamage = 50;
    [SerializeField] private float _moveStopDistance = 1.5f;
    [SerializeField] private float _explosionDelay = 1f;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private AudioSource _explosionSFX;
    private bool _wasProvoked = false;

    [SerializeField] private Transform _player;
    private bool _isExploding = false;

    private void Start()
    {
        _enemyLife = _maxEnemyLife;
        //seteo la distancia del nav mesh con la que puse por inspector 
        _agent.stoppingDistance = _moveStopDistance;
    }
    private void Update()
    {
        if (_isDead || _isExploding || _player == null) return;

        //calculo la distancia del jugador 
        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _activationRange || _wasProvoked)
        {
            if (distance > _moveStopDistance)
            {
                //se mueve hacia el jugador manteniendo la distancia
                Vector3 direction = (_player.position - transform.position).normalized;
                Vector3 target = _player.position - direction * _moveStopDistance;
                _agent.SetDestination(target);
                if (_animator) _animator.SetBool("isRunning", true);
            }
            else if (!_isExploding)
            {
                _agent.ResetPath();
                if (_animator) _animator.SetBool("isRunning", false);
                StartCoroutine(ExplodeAfterDelay());
            }
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        _isExploding = true;
        yield return new WaitForSeconds(_explosionDelay);

        if (_explosionSFX) _explosionSFX.Play();
        
        _explosionParticles.Play();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRange);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealing player = hit.GetComponent<PlayerHealing>();
                if (player != null)
                {
                    player.TakeDamage(_explosionDamage);
                }
            }
        }

        Death();
        Destroy(gameObject);
    }

    public void TakeHit(int damage)
    {
        _wasProvoked = true;

        GetDamage(damage);
        _animator.SetTrigger("Hit");
        if (_enemyLife <= 0 && !_isExploding)
        {
            _agent.ResetPath();
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}
