using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _shootDistance = 10f;

    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private AudioSource _shootSFX;
    [SerializeField] private AudioSource _emptyGunSFX;
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private GameObject _tracerPrefab;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private PlayerHealing _playerHealing;
    private float _lastShootTime = -Mathf.Infinity;
    private bool _canShoot = false;


    private Animator _animator;

    private PlayerShootStats _player;

    private bool _hasWeapon = false;
    public bool HasWeapon
    {
        get { return _hasWeapon; }
        set { _hasWeapon = value; }
    }
    public void SetMuzzlePoint(Transform muzzle)
    {
        _muzzlePoint = muzzle;
    }


    private void Start()
    {
        _player = GetComponentInParent<PlayerShootStats>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_playerHealing.GetPlayerLife() <= 0) return;

        if (_hasWeapon && Input.GetMouseButtonDown(0))
        {
            if (Time.time - _lastShootTime >= _shootCooldown)
            {
                _lastShootTime = Time.time;

                if (_player.MagazineSize > 0)
                {
                    _canShoot = true;
                    _animator.SetTrigger("Shoot"); // dispara la animación
                }
                else
                {
                    _emptyGunSFX.Play(); // sin balas, sonido de arma vacía
                }
            }
        }
    }

    public void Shoot()
    {
        if (!_canShoot || _player.MagazineSize <= 0)
        {
            _emptyGunSFX.Play();
            return;
        }

            _canShoot = false;
        _shootSFX.Play();
        _player.MagazineSize--;

        Ray ray = new Ray(_camera.position, _camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * _shootDistance, Color.blue, 1f);
        Vector3 endPoint = ray.origin + ray.direction * _shootDistance;

        if (Physics.Raycast(ray, out RaycastHit hit, _shootDistance))
        {
            endPoint = hit.point;
            if (((1 << hit.collider.gameObject.layer) & _enemyLayers) != 0)
            {
                IDamageEnemy enemy = hit.collider.GetComponentInParent<IDamageEnemy>();
                if (enemy != null)
                {
                    Debug.Log("Player is damaging " + hit.collider.name);
                    enemy.TakeHit(_player.GetEnemyDamage);
                }
            }
        }
        SpawnTracer(endPoint);
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