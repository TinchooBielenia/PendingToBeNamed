using UnityEngine;

//TP2 - Martin Bielenia - Juliana Dimeglio
public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private AudioSource _pickUpGunSFX;
    [SerializeField] private bool _isPickup = false;
    private Player _player;

    public bool IsPickUp => _isPickup;

    public bool GunPickedUp()
    {
        _isPickup = true;
        _pickUpGunSFX.Play();
        return _isPickup;
    }

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_isPickup)
        {
            PlaceGunOnHand();
        }
    }

    private void PlaceGunOnHand()
    {
        // GunSocket from the active avatar
        PlayerAvatarConfiguration avatarConfig = _player.GetComponentInChildren<PlayerAvatarConfiguration>();
        if (avatarConfig == null || avatarConfig.gunSocket == null) return;

        // Instantiate the weapon in the right GunSocket
        GameObject newWeapon = Instantiate(_weaponPrefab, avatarConfig.gunSocket);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        _isPickup = false;

        // Find the muzzlePoint
        Transform muzzlePoint = avatarConfig.gunSocket;

        PlayerShoot _playerShoot = _player.GetComponent<PlayerShoot>();
        if (_playerShoot != null)
        {
            _playerShoot.HasWeapon();
            if (muzzlePoint != null)
            {
                _playerShoot.SetMuzzlePoint(muzzlePoint);
            }
        }
    }
}

