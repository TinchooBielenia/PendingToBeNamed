using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HintController _firstHint;
    public static GameManager Instance { get; private set; }

    public int SelectedCharacterID { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


    }

    private void Start()
    {
        StartCoroutine(InitFirstHint());
    }

    private IEnumerator InitFirstHint()
    {
        yield return new WaitForSecondsRealtime(5);
        _firstHint = HintController.Instance;

        if (_firstHint != null)
            _firstHint.ShowHint();
        else
            Debug.LogWarning("HintController no encontrado.");
    }

    public void SetSelectedCharacter(int id)
    {
        SelectedCharacterID = id;
    }
}
