using UnityEngine;

public class ShedDoor : MonoBehaviour
{
    private Animator _doorAnimation;
    //[SerializeField] private AudioSource _openedDoorSFX;
    [SerializeField] private int _noteID;

    private void Start()
    {
        _doorAnimation = GetComponent<Animator>();
    }

    private void OpenDoor(int id)
    {
        if (id == _noteID)
        {
            //_openedDoorSFX.Play();
            _doorAnimation.SetTrigger("isOpened");
        }
    }

    private void OnEnable()
    {
        NoteController.NoteOpened -= OpenDoor;
        NoteController.NoteOpened += OpenDoor;
    }

    private void OnDisable()
    {
        NoteController.NoteOpened -= OpenDoor;
    }
}
