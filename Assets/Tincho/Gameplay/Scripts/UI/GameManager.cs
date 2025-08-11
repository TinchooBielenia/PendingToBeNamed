using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   public static GameManager Instance { get; private set; }

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

    public void SetSelectedCharacter(int id)
    {
        SelectedCharacterID = id;
    }
}
