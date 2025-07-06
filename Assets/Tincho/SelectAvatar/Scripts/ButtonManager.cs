using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _sophieLight;
    [SerializeField] private GameObject _adamLight;
    [SerializeField] private AudioSource _hoverSFX;


    private void Start()
    {
        _sophieLight.SetActive(false);
        _adamLight.SetActive(false);
    }

    public void MouseOnSophie()
    {
        _adamLight.SetActive(false);
        _sophieLight.SetActive(true);
    }
    
    public void MouseOnAdam()
    {
        _adamLight.SetActive(true);
        _sophieLight.SetActive(false);
    }

    public void MouseOnHoverSFX()
    {
        _hoverSFX.Play();
    }
}
