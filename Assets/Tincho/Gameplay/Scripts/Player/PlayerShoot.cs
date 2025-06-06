using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _shootDistance = 10f;

    //Variable used to make a reference to the "enemy" layer mask.
    [SerializeField] private int _enemyLayer;
    [SerializeField] private AudioSource _shootSFX;
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private GameObject _tracerPrefab;
    [SerializeField] private Transform _muzzlePoint;
    private float _lastShootTime = -Mathf.Infinity;

    private Animator _animator;

    private PlayerShootStats _player;

    private bool _hasWeapon = false;
    public bool GetHasWeapon => _hasWeapon;

    public void SetHasWeapon(bool value)
    {
        _hasWeapon = value;
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
        if (_hasWeapon && _player.magazineSize > 0 && Input.GetMouseButtonDown(0))
        {
            if (Time.time - _lastShootTime >= _shootCooldown)
            {
                _animator.SetTrigger("Shoot");
                _lastShootTime = Time.time;
            }
        }
    }

    public void Shoot()
    {
        if (_player.magazineSize <= 0)
            return;

        _shootSFX.Play();
        _player.magazineSize--;

        Ray ray = new Ray(_camera.position, _camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * _shootDistance, Color.blue, 1f);
        Vector3 endPoint = ray.origin + ray.direction * _shootDistance;

        if (Physics.Raycast(ray, out RaycastHit hit, _shootDistance))
        {
            endPoint = hit.point;
            if (hit.collider.gameObject.layer == _enemyLayer)
            {
                IDamageEnemy enemy = hit.collider.GetComponentInParent<IDamageEnemy>();
                if (enemy != null)
                {
                    Debug.Log("Player is damaging " + hit.collider.name);
                    enemy.TakeHit(_player.enemyDamage);
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