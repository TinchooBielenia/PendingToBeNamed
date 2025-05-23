using UnityEngine;

public class ShootRaycast : MonoBehaviour
{
    [SerializeField]
    float _maxDistance;

    [SerializeField]
    LayerMask _layerMask;
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit[] raycastHits = Physics.RaycastAll(ray, _maxDistance, _layerMask);

        //Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.blue);
        //RaycastHit raycastHit;
        //bool raycast = Physics.Raycast(ray, out raycastHit, _maxDistance, _layerMask);

        if (raycastHits.Length > 0)
        {
            foreach (var hit in raycastHits)
            {
                Debug.Log("Hit " + hit.transform.name);
            }
        }
    }
}
