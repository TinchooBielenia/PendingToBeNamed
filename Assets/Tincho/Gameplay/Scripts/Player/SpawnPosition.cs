using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    private Vector3 _spawnPoint = new Vector3(106.35f, 3.34f, 41.98f);
    [SerializeField] private Transform _player;

    private void Start()
    {
        _player.localPosition = _spawnPoint;
    }
}
