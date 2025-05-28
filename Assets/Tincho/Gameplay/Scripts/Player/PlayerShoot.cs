using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _shootDistance = 10f;
    
    //Variable used to make a reference to the "enemy" layer mask.
    [SerializeField] private int _enemyLayer;
    [SerializeField] private AudioSource _shootSFX;

    private PlayerShootStats _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerShootStats>();
    }

    void Update()
    {
        if (_player.magazineSize > 0 && Input.GetMouseButtonDown(0))
        {
             Shoot();
        }
    }

    private void Shoot()
    {
        _shootSFX?.Play();
        _player.magazineSize--;

        Ray ray = new Ray(_camera.position, _camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * _shootDistance, Color.blue, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, _shootDistance))
        {
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
    }

}