using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Collider))]

public class BaseInteractableObject : MonoBehaviour
{
    [SerializeField] protected GameObject _circleCanvas;
    [SerializeField] protected float _detectionRadius;
    [SerializeField] protected Transform _playerTransform;

    private bool _playerInRange = false;

    private void Start()
    {
        if (_circleCanvas != null)
            _circleCanvas.SetActive(false);
    }

    private void Update()
    {
        DetectPlayerProximity();
    }

    private void DetectPlayerProximity()
    {
        if (_playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (distance <= _detectionRadius && !_playerInRange)
        {
            _playerInRange = true;
            _circleCanvas?.SetActive(true);
        }
        else if (distance > _detectionRadius && _playerInRange)
        {
            _playerInRange = false;
            _circleCanvas?.SetActive(false);
        }
    }
}
