using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TP2 - Martin Bielenia
public class SkyboxRolling : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    void Update()
    {
        transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
    }
}
