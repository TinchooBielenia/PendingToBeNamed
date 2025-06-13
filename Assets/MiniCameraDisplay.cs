using System.Collections;
using UnityEngine;

public class MiniCameraDisplay : MonoBehaviour
{
    [SerializeField] private Camera victoryCamera;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private Vector2 viewportPosition = new Vector2(0.75f, 0.75f);
    [SerializeField] private Vector2 viewportSize = new Vector2(0.25f, 0.25f);
    [SerializeField] private float transitionDuration = 0.5f;

    private Rect originalRect;

    private void Start()
    {
        if (victoryCamera != null)
        {
            originalRect = victoryCamera.rect;
            victoryCamera.gameObject.SetActive(false);
        }
    }

    public void ShowVictoryCamera()
    {
        if (victoryCamera != null)
        {
            StartCoroutine(ShowCameraSmooth());
        }
    }

    private IEnumerator ShowCameraSmooth()
    {
        victoryCamera.gameObject.SetActive(true);

        Rect startRect = new Rect(viewportPosition.x, viewportPosition.y, 0f, 0f);
        Rect endRect = new Rect(viewportPosition.x, viewportPosition.y, viewportSize.x, viewportSize.y);

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionDuration);
            victoryCamera.rect = LerpRect(startRect, endRect, t);
            yield return null;
        }
        victoryCamera.rect = endRect;

        yield return new WaitForSeconds(displayDuration);

        elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionDuration);
            victoryCamera.rect = LerpRect(endRect, startRect, t);
            yield return null;
        }

        victoryCamera.rect = originalRect;
        victoryCamera.gameObject.SetActive(false);
    }

    private static Rect LerpRect(Rect a, Rect b, float t)
    {
        return new Rect(
            Mathf.Lerp(a.x, b.x, t),
            Mathf.Lerp(a.y, b.y, t),
            Mathf.Lerp(a.width, b.width, t),
            Mathf.Lerp(a.height, b.height, t)
        );
    }
}
