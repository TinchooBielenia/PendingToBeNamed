using UnityEngine;
using UnityEngine.UI;

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
        //casts a ray from the camera to check the distance 
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _checkDistance, _interactLayer))
        {
            //if the object is interactable then it will change the size of the crosshair.
            if (hit.collider.GetComponent<IInteraction>() != null)
            {
                _crosshairImage.transform.localScale = _hoverScale;
                _interactCanvas.SetActive(true);
                return;
            }
        }
        //if the object is not interactable reset the size 
        _crosshairImage.transform.localScale = _normalScale;
        _interactCanvas.SetActive(false);

    }
}
