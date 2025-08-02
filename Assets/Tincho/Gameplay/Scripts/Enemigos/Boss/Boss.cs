using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy, IDamageEnemy
{

    private BossState currentState;
    public Transform player;
    public Animator animator;
    public AudioSource damageSFX;
    public AudioSource rifleSFX;

    [Header("HordeSpawn")]
    public EnemySpawner enemySpawner;
    public int zombiesToSpawn = 5;
    private bool _hasSummonedZombies = false;
    public Image _lifeBar;
    public GameObject _lifeBarPrefab;
    public EnemyAI _enemyAI;
    public PlayerHealth _playerHealth;

    [Header("Weapon")]
    public GameObject weaponObject;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool isShooting = false;
    public bool isPunching = false;

    [Header("MeleeAttack")]
    public float meleeRange = 1.5f;
    public int meleeDamage = 10;
    public float meleeCooldown = 1.5f;
    [HideInInspector] public float lastMeleeTime = -10f;
    private float _stateChangeCooldown = 2f; 
    private float _lastStateChangeTime = -Mathf.Infinity;
    private bool _isInvulnerable = false;
    [SerializeField] private GameObject _meleeDamageArea;
    private bool _inMelee = false;

    [Header("Bomb Spawner")]
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private float _spawnDelay = 0.3f;
    [SerializeField] private int _zombieAmount;
    public BombSpawner bombSpawner;
    public CageMetallicDoor cage;


    private void Start()
    {
        base.Awake();
        _enemyLife = _maxEnemyLife;
        ChangeState(new RangedAttackState(this));
    }

    private void Update()
    {
        if (_isDead) return;

        currentState?.Tick();

        if (Time.time - _lastStateChangeTime < _stateChangeCooldown)
            return;

        if (_enemyLife <= _maxEnemyLife * 0.3f && !(currentState is MeleeAtackState))
        {
            ChangeState(new MeleeAtackState(this));
            _inMelee = true;
        }

        if (_enemyLife <= _maxEnemyLife * 0.6f && !_hasSummonedZombies)
        {
            _hasSummonedZombies = true;
            Debug.Log("Invocando zombis");
            ChangeState(new BossSummonZombies(this));
        }

        RotateTowardsPlayer();
    }


    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f; 

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            if (isShooting)
            {
                lookRotation *= Quaternion.Euler(0f, 82.018f, 0f);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
    public void ChangeState(BossState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        _lastStateChangeTime = Time.time;
    }

    protected override void Death()
    {
        if (_isDead) return;

        if (_enemyLife <= 0)
        {
            _isDead = true;
            cage.OpenMainGate();
            animator.SetTrigger("Dead");

            currentState = null; 
            StopAllCoroutines(); 
            isShooting = false;  
            animator.SetBool("IsShooting", false);

            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
            LootOnDeath();
            _lifeBarPrefab.SetActive(false);
            Destroy(gameObject, 5f);
        }
    }

    public void TakeHit(int damage)
    {
        if (_isDead) return ;
        if (_isInvulnerable) return;
        if (_inMelee)
        {
            animator.SetBool("IsPunching", false);
            animator.SetTrigger("MeleHit");
        }
        if (isShooting)
        {
            animator.SetBool("IsShooting", false);
            animator.SetTrigger("RangeHit");
        }
        GetDamage(damage);
        damageSFX.Play();
        _damageParticles1.Play();
        _damageParticles2.Play();
        ManageLifeBar();
        Death();
    }
    public void ManageLifeBar()
    {
        _lifeBar.fillAmount = _enemyLife / _maxEnemyLife;
    }
    public void SetWeaponActive(bool isActive)
    {
        if (weaponObject != null)
            weaponObject.SetActive(isActive);
    }
    public void EnableDamageCollider()
    {
        _meleeDamageArea.SetActive(true); 
    }

    public void DisableDamageCollider()
    {
        _meleeDamageArea.SetActive(false);
    }
}
