using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerShootStats : MonoBehaviour
{
    [SerializeField] private int _magazineSize;
    [SerializeField] private int _fullMagazineSize = 10;
    [SerializeField] private int _enemyDamage;
    [SerializeField] private AudioSource _ammoBoxSFX;
    [SerializeField] private Image _bullet;
    [SerializeField] private TextMeshProUGUI _bulletCounter;

    public int MagazineSize
    {
        get { return _magazineSize; }
        set { _magazineSize = value; }
    }

    public int FullMagazineSize
    {
        get { return _fullMagazineSize; }
        set { _fullMagazineSize = value; }
    }

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

}
