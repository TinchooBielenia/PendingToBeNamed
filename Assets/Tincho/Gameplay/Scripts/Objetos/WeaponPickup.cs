using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Transform _handBone;
    [SerializeField] private Transform _gunSocket;
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private AudioSource _pickUpSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _pickUpSFX != null)
        {
            Player _player = other.GetComponent<Player>();

            _pickUpSFX.Play();
            GameObject newWeapon = Instantiate(_weaponPrefab, _handBone);
            Transform transform1 = newWeapon.transform;
            transform1.SetParent(_gunSocket);
            transform1.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            Transform muzzlePoint = newWeapon.transform.Find("muzzlePoint");

            PlayerShoot _playerShoot = _player.gameObject.GetComponent<PlayerShoot>();
            _playerShoot.HasWeapon = true;
            if (_playerShoot != null && muzzlePoint != null)
            {
                _playerShoot.SetMuzzlePoint(muzzlePoint);
            }
            Destroy(gameObject);
        }

    }
}

