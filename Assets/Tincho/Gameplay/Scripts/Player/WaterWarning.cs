using UnityEngine;
using UnityEngine.Rendering;

public class WaterWarning : MonoBehaviour
{
    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _waterLayer;
    [SerializeField] private float _sphereHeight;
    private PlayerHealth _playerHealth;

    private bool _isNearWater = false;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (_playerHealth.PlayerDeath()) return;

        Vector3 feetPosition = transform.position + Vector3.up * _sphereHeight;
        Collider[] hits = Physics.OverlapSphere(feetPosition, _detectionRadius, _waterLayer);

        bool detected = hits.Length > 0;

        if (detected && !_isNearWater && !_playerHealth.PotionDrunk)
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

