
using UnityEngine;

public class TankEnemy : Enemy, IDamageEnemy   
{
    private bool _inRange;
    [SerializeField] private Transform _player;
    [SerializeField] private int _speed;
    [SerializeField] private int _maxSpeed;
    [SerializeField] private Rigidbody _enemyRb;
    private Animator _animator;
    private bool _isMoving;

    private void Start()
    {
        _enemyLife = _maxEnemyLife;
        _inRange = false;
        _speed = _maxSpeed;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_inRange && _player != null)
        {
            DashAndStop(_player, _speed);
        }

        if(_isDead)
        {
            if (_animator != null)
            {
                _animator.SetBool("isDead", true);
                _inRange = false;
                _enemyRb.isKinematic = true;
            }
        }
    }

    public void TakeHit(int damage)
    {
        GetDamage(damage);
        Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("El jugador entró en la zona del enemigo.");
            _inRange = true;
            _animator.SetBool("isRunning", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("El jugador salió en la zona del enemigo.");
            _inRange = false;
            _animator.SetBool("isRunning", false);
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
        _isMoving = dir.magnitude != 0;

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Player player = collision.gameObject.GetComponent<Player>();
    //    if (player != null)
    //    {
    //        Debug.Log("El enemigo chocó físicamente al jugador.");
    //        //OnTouchPlayer(player);
    //    }
    //}

    //private void OnTouchPlayer(Player player)
    //{

    //}
}