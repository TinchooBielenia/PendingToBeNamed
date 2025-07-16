using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private int fireDamage = 5;
    [SerializeField] private float duration = 4f;
    private float _trapTimer;
    private float _trapDamageTimer;
    [SerializeField] private float _fullTrapDamageTimer;
    private bool _isTrapped = false;
    private bool _isActive = false;
    private BoxCollider _collider;
    private Enemy _enemy;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        _trapDamageTimer = 0;
    }

    public void Activate()
    {
        Debug.Log("Se activo la trampa");
        _isActive = true;
        _trapTimer = duration;
        _collider.enabled = true;
        OnEnable();
    }

    private void Update()
    {
        if (!_isActive) return;

        _trapTimer -= Time.deltaTime;
        if (_trapTimer <= 0f)
        {
            _isActive = false;
            _collider.enabled = false;
            _isTrapped = false;
            _trapDamageTimer = _fullTrapDamageTimer;
            OnDisable();
        }

        if (_isTrapped)
        {
            _trapDamageTimer -= Time.deltaTime;

            if (_trapDamageTimer <= 0f)
            {
                TryApplyDamage(_enemy, new DamageData(fireDamage, DamageType.Fire));
                _trapDamageTimer = _fullTrapDamageTimer;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            _enemy = other.GetComponentInParent<Enemy>();
            _isTrapped = true;
        }
    }

    private void TryApplyDamage<T>(Enemy col, T damage)
    {
        var target = col.GetComponentInParent<IDamageEnemy<T>>();
        if (target != null)
        {
            target.TakeHit(damage);

            if (col.IsDead)
            {
                _enemy = null;
                _isTrapped = false;
            }
        }
    }
    private void OnEnable()
    {
        TrapButton.OnButtonPressed += HandleTrapActivation;
    }

    private void OnDisable()
    {
        TrapButton.OnButtonPressed -= HandleTrapActivation;
    }

    private void HandleTrapActivation(FireTrap trap)
    {
        if (trap == this)
        {
            Activate();
        }
    }

}