using UnityEngine;

public class AvatarApplier : MonoBehaviour
{
    [SerializeField] private GameObject _skinChica;
    [SerializeField] private Avatar _avatarChica;

    [SerializeField] private GameObject _skinChico;
    [SerializeField] private Avatar _avatarChico;

    [SerializeField] private Animator _animator;

    private void Start()
    {
        int selectedID = GameManager.Instance.SelectedCharacterID;

        _skinChica.SetActive(selectedID == 0);
        _skinChico.SetActive(selectedID == 1);

        if (selectedID == 0)
        {
            _animator.avatar = _avatarChica;
        }
        else if (selectedID == 1)
        {
            _animator.avatar = _avatarChico;
        }
    }
}
