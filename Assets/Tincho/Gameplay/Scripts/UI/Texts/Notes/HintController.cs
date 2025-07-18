using System.Collections;
using TMPro;
using UnityEngine;

public class HintController : MonoBehaviour
{
    public static HintController Instance;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private TextMeshProUGUI _hintValue;
    [SerializeField] private float _hideTimer;
    [SerializeField] private AudioSource _hintSFX;

    private GameObject _player;

    private Coroutine _hideCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _player = Player.Instance.gameObject;
    }

    public void ShowHint(int hintID, NotesTextContainer.NoteType type)
    {
        // Si el jugador está desactivado, ocultamos cualquier hint y no mostramos nada
        if (!_player.activeSelf)
        {
            HideHintImmediate();
            return;
        }

        _canvas.SetActive(true);
        _hintSFX.Play();
        _hintValue.text = NotesTextContainer.GetTextByID(hintID, type);

        // Reiniciamos el timer si ya estaba corriendo
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }

        _hideCoroutine = StartCoroutine(HideHintAfterDelay());
    }

    private void HideHintImmediate()
    {
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
            _hideCoroutine = null;
        }

        _canvas.SetActive(false);
    }

    private IEnumerator HideHintAfterDelay()
    {
        float timer = 0f;

        while (timer < _hideTimer)
        {
            if (!_player.activeSelf)
            {
                _canvas.SetActive(false);
                yield break;
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        _canvas.SetActive(false);
    }
}
