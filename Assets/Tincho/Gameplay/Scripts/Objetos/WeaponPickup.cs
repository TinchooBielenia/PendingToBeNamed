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
            Player _player = other.GetComponent<Player>();

            pickUpSFX.Play();
            GameObject newWeapon = Instantiate(weaponPrefab, handBone);
            newWeapon.transform.SetParent(gunSocket);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;

            Transform muzzlePoint = newWeapon.transform.Find("muzzlePoint");

            PlayerShoot _playerShoot = _player.gameObject.GetComponent<PlayerShoot>();
            _playerShoot.SetHasWeapon(true);
            if (_playerShoot != null && muzzlePoint != null)
            {
                _playerShoot.SetMuzzlePoint(muzzlePoint);
            }
            Destroy(gameObject);
        }

    }
}

