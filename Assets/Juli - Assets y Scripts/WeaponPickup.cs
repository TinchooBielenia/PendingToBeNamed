using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;     // Prefab del arma real
    [SerializeField] private Transform handSocket;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats == null) return;

            if (handSocket == null)
            {
                Debug.LogWarning("HandSocket no encontrado.");
                return;
            }

            weaponPrefab.transform.SetParent(handSocket);

          //  weaponPrefab.GetComponent<Collider>().enabled = false;
            weaponPrefab.GetComponent<Rigidbody>().isKinematic = true;

           // stats.hasWeapon = true;

            Destroy(gameObject); //  esto destruye el arma en el suelo
        }
    }

}
