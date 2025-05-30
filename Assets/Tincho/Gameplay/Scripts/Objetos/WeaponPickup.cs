using UnityEngine;

public class WeaponPickup : MonoBehaviour, IInteraction
{
    [SerializeField] private Transform handBone;
    [SerializeField] private Transform gunSocket;
    [SerializeField] private GameObject weaponPrefab;
    private bool _isInteracting = false;
    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void Update()
    {
        if (_isInteracting)
        {
            _isInteracting = false;

            GameObject newWeapon = Instantiate(weaponPrefab, handBone);
            newWeapon.transform.SetParent(gunSocket);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;

            Transform muzzlePoint = newWeapon.transform.Find("muzzlePoint");


            PlayerShoot playerShoot = FindObjectOfType<PlayerShoot>();
            playerShoot.SetHasWeapon(true);
            if (playerShoot != null && muzzlePoint != null)
            {
                playerShoot.SetMuzzlePoint(muzzlePoint);
            }
            Destroy(gameObject);
        }
    }
}

