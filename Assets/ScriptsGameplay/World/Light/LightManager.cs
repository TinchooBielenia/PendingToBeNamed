using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//TP2 - Juliana Dimeglio
public class LightManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float activationDistance = 20f;
    [SerializeField] private float checkInterval = 0.5f;

    [SerializeField] private List<Light> _lights = new List<Light>();

    void Start()
    {
        StartCoroutine(LightChecker());
    }
    IEnumerator LightChecker()
    {
        while (true)
        {
            CheckLights();
            yield return new WaitForSeconds(checkInterval);
        }
    }

    void CheckLights()
    {
        foreach (var light in _lights)
        {
            //returns the distance between the player position and the light.
            float dist = Vector3.Distance(player.position, light.transform.position);
            //if the distance between the player and the light is less than activationDistance the light turns on.
            light.enabled = dist < activationDistance;
        }
    }
}
