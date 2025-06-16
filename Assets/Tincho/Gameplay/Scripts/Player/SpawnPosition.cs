using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnPoint = new Vector3();
    [SerializeField] private Transform _player;

    private void Start()
    {
        _player.localPosition = _spawnPoint;
    }
}