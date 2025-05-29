using UnityEngine;

public class MedicNote : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject canvas;
    private bool _isInteracting = false;
    private bool _isCanvasVisible = false;

    private void Update()
    {
        if (_isInteracting && !_isCanvasVisible)
        {
            ShowNote();
        }

        if (_isCanvasVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            HideNote();
        }
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void ShowNote()
    {
        _isCanvasVisible = true;
        canvas.SetActive(true);
        Time.timeScale = 0f;
        _isInteracting = false; 
    }

    private void HideNote()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
        _isCanvasVisible = false;
    }
}
