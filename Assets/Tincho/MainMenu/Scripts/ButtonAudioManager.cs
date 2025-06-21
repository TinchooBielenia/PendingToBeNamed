using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudioManager : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioSource _hoverAudio;
    [SerializeField] private AudioSource _clickAudio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_hoverAudio != null)
        {
            _hoverAudio.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_clickAudio != null)
        {
            _clickAudio.Play();
        }
    }
}
