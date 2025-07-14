using UnityEngine;

//TP2 - Martin Bielenia - Juliana Dimeglio

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private bool _isPickup = false;
    private Player _player;

    public bool IsPickUp => _isPickup;

    public bool GunPickedUp()
    {
        _isPickup = true;
        return true;
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
        // GunSocket desde el avatar activo
        PlayerAvatarConfiguration avatarConfig = _player.GetComponentInChildren<PlayerAvatarConfiguration>();
        if (avatarConfig == null || avatarConfig.GunSocket == null) return;

        // Instanciar el arma en el GunSocket correcto
        GameObject newWeapon = Instantiate(_weaponPrefab, avatarConfig.GunSocket);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        _isPickup = false;

        // Buscar el muzzlePoint
        Transform muzzlePoint = avatarConfig.GunSocket;

        PlayerShoot _playerShoot = _player.GetComponent<PlayerShoot>();
        if (_playerShoot != null)
        {
            _playerShoot.HasWeapon = true;
            if (muzzlePoint != null)
            {
                _playerShoot.SetMuzzlePoint(muzzlePoint);
            }
        }
    }
}

