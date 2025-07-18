using System.Collections;
using TMPro;
using UnityEngine;
using static NoteController;

public class HintController : MonoBehaviour
{
    public static HintController Instance;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private TextMeshProUGUI _hintValue;
    [SerializeField] private int _hintID;
    [SerializeField] private NotesTextContainer.NoteType _textType;
    [SerializeField] private float _hideTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowHint()
    {
        _canvas.SetActive(true);
        _hintValue.text = NotesTextContainer.GetTextByID(_hintID, _textType);
        StartCoroutine(HideNote());
    }

    private IEnumerator HideNote()
    {
        yield return new WaitForSecondsRealtime(_hideTimer);
        _canvas.SetActive(false);
    }
}
