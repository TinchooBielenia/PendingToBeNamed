using System.Collections;
using UnityEngine;

public class HealthPotionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _potionPrefab;
    [SerializeField] private float _spawnInterval; 
    [SerializeField] private AudioSource _spawnPotionSFX;
    private PlayerHealth _playerHealth;
    private bool _playerInRange = false;
    private GameObject _player;
    private bool _hasSpawned;

    private void Start()
    {
        _playerHealth = Player.Instance.GetComponent<PlayerHealth>();
        _player = Player.Instance.gameObject;
    }

    private void Update()
    {
        if (_playerHealth != null && !_playerHealth.PotionDrunk && _playerInRange && !_hasSpawned)
        {
            StartCoroutine(SpawnPotionLoop());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player) _playerInRange = true;
        Debug.Log("Player is in range");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _player) return;
        _playerInRange = false;
    }

    private IEnumerator SpawnPotionLoop()
    {
        _hasSpawned = true;

        while (_playerHealth != null && !_playerHealth.PotionDrunk && _playerInRange)
        {
            SpawnPotion();
            yield return new WaitForSeconds(_spawnInterval);
        }

        _hasSpawned = false;
    }

    private void SpawnPotion()
    {
        Vector3 spawnPos = transform.position;

        Instantiate(_potionPrefab, spawnPos, Quaternion.identity);
        _spawnPotionSFX.Play();
    }
}
