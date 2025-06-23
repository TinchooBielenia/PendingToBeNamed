using System.Collections;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    [SerializeField] private Camera _victoryCamera;
    [SerializeField] private float _displayDuration = 3f;
    [SerializeField] private Vector2 _cameraPosition = new Vector2(0f, 0f);
    [SerializeField] private Vector2 _cameraSize = new Vector2(1f, 1f);
    [SerializeField] private Animator _mainGateAnimator;

    private Rect _ogRect;

    private void Start()
    {
        if (_victoryCamera != null)
        {
            _ogRect = _victoryCamera.rect;
            _victoryCamera.enabled = false;
        }
    }

    public void ShowVictoryCamera()
    {
        Debug.Log(_victoryCamera);
        if (_victoryCamera != null)
        {
            _victoryCamera.rect = new Rect(_cameraPosition.x, _cameraPosition.y, _cameraSize.x, _cameraSize.y);
            _victoryCamera.enabled = true;
            _mainGateAnimator.SetTrigger("mainGateOpened");
            StartCoroutine(HideAfterDelay());
        }
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(_displayDuration);
        _victoryCamera.enabled = false;
        _victoryCamera.rect = _ogRect;
    }
}