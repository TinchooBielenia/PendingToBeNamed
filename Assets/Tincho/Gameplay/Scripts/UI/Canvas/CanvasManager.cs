using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private BaseCanvas _currentPanel;

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

    public bool IsAnyCanvasOpen() => _currentPanel != null;
}
