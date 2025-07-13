using UnityEngine;

//TP2 - Martin Bielenia
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private BaseCanvas _currentPanel;

    public bool IsAnyCanvasOpen() => _currentPanel != null;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    private void Update()
    {
        if (_currentPanel != null && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCurrentCanvas();
        }
    }

    public void OpenCanvas(BaseCanvas panel)
    {
        if (_currentPanel != null) _currentPanel.Close();

        _currentPanel = panel;
        _currentPanel.Open();
    }

    public void CloseCurrentCanvas()
    {
        if (_currentPanel != null)
        {
            _currentPanel.Close();
            _currentPanel = null;
        }
    }

}
