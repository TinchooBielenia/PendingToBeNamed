using UnityEngine;
using UnityEngine.UI;

//TP2 - Juliana Dimeglio
public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;
    [SerializeField] private float _checkDistance = 3f;
    [SerializeField] private LayerMask _interactLayer;

    [Header("Sizes:")]
    [SerializeField] private Vector3 _normalScale = Vector3.one;
    [SerializeField] private Vector3 _hoverScale = new Vector3(1.5f, 1.5f, 1f);

    [SerializeField] private GameObject _interactCanvas;


    private Camera _mainCamera;
    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _checkDistance, _interactLayer))
        {
            if (hit.collider.GetComponent<IInteraction>() != null)
            {
                _crosshairImage.transform.localScale = _hoverScale;
                _interactCanvas.SetActive(true);
                return;
            }
        }
        _crosshairImage.transform.localScale = _normalScale;
        _interactCanvas.SetActive(false);

    }
}
