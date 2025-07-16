using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private int _fireDamage = 5;
    [SerializeField] private float _duration = 4f;
    private float _trapTimer;
    private float _trapDamageTimer;
    [SerializeField] private float _fullTrapDamageTimer;
    private bool _isActive = false;
    private BoxCollider _collider;
    private List<Enemy> _enemiesInside = new List<Enemy>();
    [SerializeField] List<ParticleSystem> _particles;
    private bool _turnOnParticles = false;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        _trapDamageTimer = 0;
    }

    private void Start()
    {
        TrapButton.OnButtonPressed += HandleTrapActivation;
    }

    private void Activate()
    {
        Debug.Log("Se activo la trampa");
        _isActive = true;
        _trapTimer = _duration;
        _collider.enabled = true;
        _turnOnParticles = true;
        TurnOnParticles();
    }

    private void Update()
    {
        if (!_isActive) return;


        _trapTimer -= Time.deltaTime;
        if (_trapTimer <= 0f)
        {
            Debug.Log("Se desactivo la trampa");
            _isActive = false;
            _collider.enabled = false;
            _trapDamageTimer = _fullTrapDamageTimer;
            _turnOnParticles = false;
            TurnOnParticles();
        }

        if (_enemiesInside.Count > 0)
        {
            _trapDamageTimer -= Time.deltaTime;

            if (_trapDamageTimer <= 0f)
            {
                foreach (Enemy enemy in _enemiesInside.ToArray())
                {
                    if (enemy == null || enemy.IsDead)
                    {
                        _enemiesInside.Remove(enemy);
                        continue;
                    }

                    TryApplyDamage(enemy, new DamageData(_fireDamage, DamageType.Fire));
                }

                _trapDamageTimer = _fullTrapDamageTimer;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null && !_enemiesInside.Contains(enemy))
        {
            _enemiesInside.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null && _enemiesInside.Contains(enemy))
        {
            _enemiesInside.Remove(enemy);
        }
    }

    private void TryApplyDamage<T>(Enemy enemy, T damage)
    {
        var target = enemy.GetComponentInParent<IDamageEnemy<T>>();
        if (target != null)
        {
            target.TakeHit(damage);
        }
    }

    private void TurnOnParticles()
    {
        if (_turnOnParticles)
        {
            foreach (var particle in _particles)
            {
                if (particle != null && _turnOnParticles)
                {
                    particle.Play();
                }
                else
                {
                    particle.Stop();
                }
            }
        }
        else
        {
            foreach (var particle in _particles)
            {
                if (particle != null)
                {
                    particle.Stop();
                }
            }
        }
        
    }
    private void OnDestroy()
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