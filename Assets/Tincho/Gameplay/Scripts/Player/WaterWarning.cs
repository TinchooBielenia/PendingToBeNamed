using UnityEngine;
using UnityEngine.Rendering;

public class WaterWarning : MonoBehaviour
{
    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _waterLayer;
    [SerializeField] private float _sphereHeight;
    private PlayerHealth _potionDrunk;
    //[SerializeField] private AudioSource _warningSFX;

    private bool _isNearWater = false;

    private void Start()
    {
        _potionDrunk = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        Vector3 feetPosition = transform.position + Vector3.up * _sphereHeight;
        Collider[] hits = Physics.OverlapSphere(feetPosition, _detectionRadius, _waterLayer);

        bool detected = hits.Length > 0;

        if (detected && !_isNearWater && !_potionDrunk.PotionDrunk)
        {
            _isNearWater = true;
            Debug.Log("¡Estás cerca del agua!");
            DialogsTextsUI.Instance.ShowDialogTexts(1);
        }
        else if (!detected && _isNearWater)
        {
            _isNearWater = false;
            Debug.Log("Te alejaste del agua");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 feetPosition = transform.position + Vector3.up * _sphereHeight;
        Gizmos.DrawWireSphere(feetPosition, _detectionRadius);
    }
}

