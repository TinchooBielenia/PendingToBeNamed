using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerShootStats : MonoBehaviour
{
    public int magazineSize;
    public int fullMagazineSize = 10;
    public int enemyDamage;
    [SerializeField] private AudioSource _ammoBoxSFX;
    [SerializeField] private Image _bullet;
    [SerializeField] private TextMeshProUGUI _bulletCounter;

    void Start()
    {
        enemyDamage = 10;
    }

    private void Update()
    {
        ManageBulletCounter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (magazineSize != fullMagazineSize)
        {
            if (other.CompareTag("Ammo"))
            {
                _ammoBoxSFX.Play();
                Debug.Log("El jugador recogió munición.");
                Destroy(other.gameObject); // Destruye la caja de munición
                magazineSize = fullMagazineSize;
            }
        }
        
    }

    private void ManageBulletCounter()
    {
        _bulletCounter.SetText(magazineSize.ToString());
    }

}
