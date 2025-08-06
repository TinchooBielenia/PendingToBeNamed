using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnPoint = new Vector3();
    private Transform _player;

    private void Awake()
    {
        _player = GetComponent<Transform>();
    }

    private void Start()
    {
        _player.localPosition = _spawnPoint;
    }
}