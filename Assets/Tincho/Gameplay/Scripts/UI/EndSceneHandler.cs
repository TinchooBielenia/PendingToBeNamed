using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneHandler : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private string _mainMenuScene = "01_MainMenu";

    private void Start()
    {
        if (_mainMenuButton != null)
            _mainMenuButton.onClick.AddListener(MainMenuButton);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
}
