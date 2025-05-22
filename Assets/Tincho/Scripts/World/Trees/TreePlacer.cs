using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private int numberOfTrees = 100;
    [SerializeField] private Vector3 areaSize = new Vector3(100, 50, 100); // Tamaño del área para buscar posiciones
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        PlaceTrees();
    }

    private void PlaceTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                areaSize.y,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            Vector3 rayOrigin = transform.position + randomPos;
            Ray ray = new Ray(rayOrigin, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, areaSize.y * 2, groundMask))
            {
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                Instantiate(treePrefab, hit.point, rotation, transform);
            }
        }
    }
}
