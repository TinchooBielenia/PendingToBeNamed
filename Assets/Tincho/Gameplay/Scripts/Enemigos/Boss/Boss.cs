using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy, IDamageEnemy
{

    private BossState currentState;

    [Header("Referencias")]
    public Transform player;
    public Animator animator;
    public AudioSource damageSFX;
    public AudioSource rifleSFX;
    [Header("Configuración de invocación")]
    public EnemySpawner enemySpawner;
    public int zombiesToSpawn = 5;
    private bool _hasSummonedZombies = false;
    public Image _lifeBar;
    public GameObject _lifeBarPrefab;
    public EnemyAI _enemyAI;
    public PlayerHealth _playerHealth;
    [Header("Arma")]
    public GameObject weaponObject;

    [Header("Ataques")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool isShooting = false;
    public bool isPunching = false;

    [Header("Ataque cuerpo a cuerpo")]
    public float meleeRange = 1.5f;
    public int meleeDamage = 10;
    public float meleeCooldown = 1.5f;
    [HideInInspector] public float lastMeleeTime = -10f;
    private float _stateChangeCooldown = 2f; 
    private float _lastStateChangeTime = -Mathf.Infinity;
    private bool _isInvulnerable = false;
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

        // Si no pasó suficiente tiempo desde el último cambio de estado, salimos
        if (Time.time - _lastStateChangeTime < _stateChangeCooldown)
            return;

        //// Estado Melee
        //if (_enemyLife <= _maxEnemyLife * 0.3f)
        //{
        //    StartCoroutine(SpawnHorde(_zombieAmount));
        //    return;
        //}

        // Invocación
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

        BecomeTemporarilyInvulnerable(_stateChangeCooldown); 
    }

    protected override void Death()
    {
        if (_isDead) return;

        if (_enemyLife <= 0)
        {
            _isDead = true;
            cage.OpenMainGate();
            animator.SetTrigger("Dead");

            currentState = null; // Desactivar el comportamiento del boss
            StopAllCoroutines(); // Detener cualquier acción pendiente
            isShooting = false;  // Detener flags que puedan activar animaciones
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
        if (isPunching)
        {
            animator.SetBool("IsShooting", false);
            animator.SetTrigger("MeleHit");
        }else if (isShooting)
        {
            animator.SetBool("IsShooting", false);
            animator.SetTrigger("RangeHit");
        }
        GetDamage(damage);
        damageSFX.Play();
        ManageLifeBar();
        Death();
    }
    public void ManageLifeBar()
    {
        _lifeBar.fillAmount = _enemyLife / _maxEnemyLife;
    }
    public void BecomeTemporarilyInvulnerable(float duration)
    {
        if (!_isInvulnerable)
            StartCoroutine(InvulnerabilityCoroutine(duration));
    }

    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        _isInvulnerable = false;
    }
    public void SetWeaponActive(bool isActive)
    {
        if (weaponObject != null)
            weaponObject.SetActive(isActive);
    }
    private IEnumerator SpawnHorde(int zombieQuantity)
    {

        for (int i = 0; i < zombieQuantity; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Vector3 spawnPos = transform.position + offset + Vector3.up * 1f;
            Instantiate(_zombiePrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(_spawnDelay);
        }
       
    }
}
