using UnityEngine;
using UnityEngine.UI;

public class PlayerHealing : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private float _playerLife;
    [SerializeField] private float _maxPlayerLife;

    [SerializeField] private ParticleSystem _healingSFX;
    [SerializeField] private bool _isHealing = false;
    [SerializeField] private AudioSource _healingAudioSFX;
    [SerializeField] private Image _lifeBar;

    void Start()
    {
        _playerLife = 80;
    }

    private void Update()
    {
        ManageLifeBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealingWater") && _playerLife < _maxPlayerLife)
        {
            if (!_isHealing)
            {
                _healingSFX.Play();
                _healingAudioSFX.Play();
                _isHealing = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HealingWater") && _playerLife < _maxPlayerLife)
        {
            _playerLife += Time.deltaTime;
        }
        else if (_playerLife >= _maxPlayerLife)
        {
            _healingSFX.Stop();
            _healingAudioSFX.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HealingWater"))
        {
            _healingSFX.Stop();
            _healingAudioSFX.Stop();
            _isHealing = false;
        }
    }

    public void ManageLifeBar()
    {
        _lifeBar.fillAmount = _playerLife / _maxPlayerLife;
    }


}
