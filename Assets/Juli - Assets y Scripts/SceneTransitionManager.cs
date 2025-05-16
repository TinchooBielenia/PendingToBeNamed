using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private string entranceTag;
    private bool positionSet = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToScene(string sceneName, string entranceTag)
    {
        this.entranceTag = entranceTag;
        positionSet = false;
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!positionSet)
        {
            GameObject entrancePoint = GameObject.FindWithTag(entranceTag);
            GameObject player = GameObject.FindWithTag("Player");
            if (entrancePoint != null && player != null)
            {
                player.transform.position = entrancePoint.transform.position;
                positionSet = true;
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
