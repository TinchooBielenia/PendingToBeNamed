using System.Collections;
using UnityEngine;

public class MiniCameraDisplay : MonoBehaviour
{
    [SerializeField] private Camera victoryCamera;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private Vector2 viewportPosition = new Vector2(0f, 0f);
    [SerializeField] private Vector2 viewportSize = new Vector2(1f, 1f);

    private Rect originalRect;

    private void Start()
    {
        if (victoryCamera != null)
        {
            originalRect = victoryCamera.rect;
            victoryCamera.enabled = false;
        }
    }

    public void ShowVictoryCamera()
    {
        Debug.Log(victoryCamera);
        if (victoryCamera != null)
        {
            victoryCamera.rect = new Rect(viewportPosition.x, viewportPosition.y, viewportSize.x, viewportSize.y);
            victoryCamera.enabled = true;
            StartCoroutine(HideAfterDelay());
        }
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        victoryCamera.enabled = false;
        victoryCamera.rect = originalRect;
    }
}