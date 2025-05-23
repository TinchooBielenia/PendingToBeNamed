using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private float _shootDistance = 10f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private AudioSource _shootSFX;

    private PlayerStats _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _shootSFX?.Play();

        Ray ray = new Ray(_origin.position, _origin.forward);
        Debug.DrawRay(ray.origin, ray.direction * _shootDistance, Color.blue, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, _shootDistance))
        {
            if (((1 << hit.collider.gameObject.layer) & _enemyLayer) != 0)
            {
                IDamageEnemy enemy = hit.collider.GetComponentInParent<IDamageEnemy>();
                if (enemy != null)
                {
                    Debug.Log("Player is damaging " + hit.collider.name);
                    enemy.TakeHit(_player.enemyDamage);
                }
            }
        }
    }

}
