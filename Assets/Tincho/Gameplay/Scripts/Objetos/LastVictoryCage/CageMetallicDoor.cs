using System.Collections;
using TMPro;
using UnityEngine;

public class CageMetallicDoor : MonoBehaviour
{
    private Animator _metallicDoorAnimation;
    private bool _puzzleWasSolved;
    [SerializeField] private AudioSource _openedDoorSFX;
    [SerializeField] private AudioSource _epicBattleMusicSFX;
    [SerializeField] private Animator _mainGate;
    [SerializeField] private GameObject _blockerDoor;
    [SerializeField] private GameObject _boss;

    private void Start()
    {
        _metallicDoorAnimation = GetComponent<Animator>();
        _blockerDoor.SetActive(false);
    }

    public bool puzzleWasSolved
    {
        set { _puzzleWasSolved = value; }
    }

    private void Update()
    {
        if (_puzzleWasSolved)
        {
            OpenMetallicDoor();
        }
    }

    private void OpenMetallicDoor()
    {
        _openedDoorSFX.Play();
        _metallicDoorAnimation.SetTrigger("doorOpened");
        _puzzleWasSolved = false;
    }

    //This is triggered when crossing the collider.
    public void FinalZoneBattle()
    {
        Debug.Log("Player entro");
        _blockerDoor.SetActive(true);
        _boss.SetActive(true);
        SceneHanlder.Instance.StartPreloadEndScene();
        _openedDoorSFX.Play();
        StartCoroutine(DelayStartMusic());
        _metallicDoorAnimation.SetTrigger("doorClosed");
        _mainGate.SetBool("closeDoor", true);
    }

    public void OpenMainGate()
    {
        Debug.Log("Player logro escapar");
        _mainGate.SetBool("closeDoor", false);
        _mainGate.SetTrigger("mainGateOpened");
        StopEpicMusic();
    }

    private IEnumerator DelayStartMusic()
    {
        yield return new WaitForSeconds(3);
        _epicBattleMusicSFX.Play();
    }

    public void StopEpicMusic()
    {
        if (_epicBattleMusicSFX.isPlaying)
            StartCoroutine(FadeOutSound(_epicBattleMusicSFX, 3f));
    }

    private IEnumerator FadeOutSound(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restaurar volumen original por si se vuelve a usar
    }

}