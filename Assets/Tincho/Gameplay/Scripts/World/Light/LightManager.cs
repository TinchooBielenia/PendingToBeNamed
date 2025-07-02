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
            //devuelve la distancia entre la posicion del player y la de la luz 
            float dist = Vector3.Distance(player.position, light.transform.position);
            //si la distancia entre el player y la luz es menor que activationDistance se prende la luz 
            light.enabled = dist < activationDistance;
        }
    }
}
