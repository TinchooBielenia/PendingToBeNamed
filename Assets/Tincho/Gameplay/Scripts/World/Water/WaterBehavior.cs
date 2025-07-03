using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    [SerializeField] private float _scrollSpeedX = 0.1f;
    [SerializeField] private float _scrollSpeedY = 0.1f;
    private Renderer _rend;
    [SerializeField] private float _healingRate;
    [SerializeField] private float _waterDamageRate;
    private PlayerHealing _player;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        WaterAnimation();
    }

    private void WaterAnimation()
    {
        float offsetX = Time.time * _scrollSpeedX;
        float offsetY = Time.time * _scrollSpeedY;
        _rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<PlayerHealing>();
            if (_player == null) return;

            if (_player.PotionDrunk && _player.GetPlayerLife() < _player.MaxPlayerLife)
            {
                _player.HealPlayerFromWater(_healingRate);
            }
            else if (!_player.PotionDrunk)
            {
                _player.TakeDamageFromWater(_waterDamageRate);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player.StopWaterEffects();
            }

            _player = null;

            Debug.Log("Player exited the water");
        }
    }


}
