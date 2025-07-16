using System;
using UnityEngine;

public class TrapButton : MonoBehaviour, IInteraction
{
    //Consigna: Delegate
    public delegate void TrapActivationDelegate(FireTrap trap);
    public static event TrapActivationDelegate OnButtonPressed;
    [SerializeField] private FireTrap _targetTrap;

    public void TriggerInteraction()
    {
        Debug.Log("Boton activado!");
        OnButtonPressed?.Invoke(_targetTrap);
    }
}