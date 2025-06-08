using UnityEngine;

public class PickableItemsMovement : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _pickableObject;

    private Vector3 pointA;
    private Vector3 pointB;

    void Start()
    {
        pointA = transform.position;
        pointB = pointA + Vector3.up * _height;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * _speed, 1f);
        transform.position = Vector3.Lerp(pointA, pointB, t);
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);

        if (_pickableObject == null)
        {
            Destroy(gameObject);
        }
    }
}
