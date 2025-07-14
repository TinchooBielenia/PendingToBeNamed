using UnityEngine;
using TMPro;
using UnityEngine.UI;

//TP2 - Martin Bielenia
public class PlayerShootStats : MonoBehaviour
{
    [SerializeField] private int _magazineSize;
    [SerializeField] private int _fullMagazineSize = 10;
    [SerializeField] private int _enemyDamage;
    [SerializeField] private AudioSource _ammoBoxSFX;
    [SerializeField] private Image _bullet;
    [SerializeField] private TextMeshProUGUI _bulletCounter;

    public int MagazineSize => _magazineSize;
    public int FullMagazineSize => _fullMagazineSize;

    public int GetEnemyDamage => _enemyDamage;
    void Start()
    {
        _enemyDamage = 10;
    }

    private void Update()
    {
        ManageBulletCounter();
    }


    private void ManageBulletCounter()
    {
        _bulletCounter.SetText(_magazineSize.ToString());
    }

    public bool TryReload()
    {
        if (_magazineSize < _fullMagazineSize)
        {
            _magazineSize = _fullMagazineSize;
            _ammoBoxSFX?.Play();
            return true;
        }

        return false;
    }

    public bool TryShoot()
    {
        if (_magazineSize > 0)
        {
            _magazineSize--;
            return true;
        }

        return false;
    }

}
