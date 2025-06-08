using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Transform handBone;
    [SerializeField] private Transform gunSocket;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private AudioSource pickUpSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && pickUpSFX != null)
        {
            pickUpSFX.Play();
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

