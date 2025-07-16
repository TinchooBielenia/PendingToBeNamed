using UnityEngine;
//TP2 - Martin Bielenia - Juliana Dimeglio
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _shootDistance = 10f;

    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private AudioSource _shootSFX;
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private GameObject _tracerPrefab;
    [SerializeField] private Transform _muzzlePoint;
    private PlayerHealth _playerHealth;
    private float _lastShootTime = -Mathf.Infinity;
    private bool _canShoot = false;

    [SerializeField] private Camera _normalCamera;
    [SerializeField] private Camera _aimingCamera;
    private bool _isAiming = false;


    private Animator _animator;

    private PlayerShootStats _playerShootingStats;

    private bool _hasWeapon = false;

    public bool HasWeapon() => _hasWeapon = true;

    public void SetMuzzlePoint(Transform muzzle)
    {
        _muzzlePoint = muzzle;
    }


    private void Start()
    {
        _playerShootingStats = GetComponentInParent<PlayerShootStats>();
        _animator = GetComponent<Animator>();
        _playerHealth = GetComponentInParent<PlayerHealth>();

        _normalCamera.enabled = true;
        _aimingCamera.enabled = false;
    }

    void Update()
    {
        if (_playerHealth.PlayerLife <= 0) return;

        IsAiming();

        if (_hasWeapon && _isAiming)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Time.time - _lastShootTime >= _shootCooldown)
                {
                    _lastShootTime = Time.time;

                    if (_playerShootingStats.MagazineSize > 0)
                    {
                        _canShoot = true;
                        Shoot();
                    }
                    else
                    {
                        Debug.Log("No bullets!");
                    }
                }
            }


        }
    }

    private void IsAiming()
    {
        if (Input.GetKey(KeyCode.Mouse1) && _hasWeapon)
        {
            _normalCamera.enabled = false;
            _aimingCamera.enabled = true;
            _isAiming = true;
            _animator.SetBool("Aiming", true);
        }
        else
        {
            _normalCamera.enabled = true;
            _aimingCamera.enabled = false;
            _isAiming = false;
            _animator.SetBool("Aiming", false);
        }
    }

    public void Shoot()
    {
        if (!_canShoot || _playerShootingStats.MagazineSize <= 0)
        {
            Debug.Log("No bullets!");
            return;
        }

        _canShoot = false;
        _shootSFX.Play();
        _playerShootingStats.TryShoot();

        Ray ray = new Ray(_camera.position, _camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * _shootDistance, Color.blue, 1f);
        Vector3 endPoint = ray.origin + ray.direction * _shootDistance;

        if (Physics.Raycast(ray, out RaycastHit hit, _shootDistance))
        {
            endPoint = hit.point;

            if (_enemyLayers.Contains(hit.collider.gameObject.layer))
            {
                TryApplyDamage(hit.collider, new DamageData(_playerShootingStats.EnemyDamage, DamageType.Bullet));
            }
        }
        SpawnTracer(endPoint);
    }

    // Generic method to apply damage of any kind.
    private void TryApplyDamage<T>(Collider collider, T damage)
    {
        var target = collider.GetComponentInParent<IDamageEnemy<T>>();
        if (target != null)
            target.TakeHit(damage);
    }
    private void SpawnTracer(Vector3 hitPoint)
    {
        GameObject tracer = Instantiate(_tracerPrefab);
        LineRenderer line = tracer.GetComponent<LineRenderer>();
        line.SetPosition(0, _muzzlePoint.position);
        line.SetPosition(1, hitPoint);
        Destroy(tracer, 0.05f);
    }

}