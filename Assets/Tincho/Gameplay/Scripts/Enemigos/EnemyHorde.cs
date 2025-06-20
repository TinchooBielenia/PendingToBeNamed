using UnityEngine;
using UnityEngine.AI;

public class EnemyHorde : MonoBehaviour, IDamageEnemy
{
    [SerializeField] private float _stunDuration = 5f;
    [SerializeField] private int _maxLife = 3;
    private int _currentLife;

    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _agent;
    private bool _isStunned = false;
    private float _stunTimer = 0f;

    [SerializeField] private Animator _animator;

    private void Start()
    {
        _currentLife = _maxLife;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
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
        bool shouldMove = distance > 0.1f;

        _animator.SetBool("isRunning", shouldMove);

        if (shouldMove)
        {
            _agent.SetDestination(_player.position);
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

    public void TakeHit(int damage)
    {
        _currentLife -= damage;

        _isStunned = true;
        _stunTimer = _stunDuration;

        if (_animator != null)
            _animator.SetBool("isRunning", false);

        if (_currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
