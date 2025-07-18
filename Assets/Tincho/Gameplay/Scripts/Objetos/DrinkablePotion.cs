using UnityEngine;

public class DrinkablePotion : MonoBehaviour, IInteraction
{
    private PotionsEffectController _potionEffect;
    private bool _isInteracting = false;
    [SerializeField] private float _destructionTimer;

    private void Start()
    {
        _potionEffect = Player.Instance.GetComponent<PotionsEffectController>();
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void Update()
    {
        if (_isInteracting)
        {
            _potionEffect.ActivatePotionEffect();
            Destroy(gameObject);
        }
        else Destroy(gameObject, _destructionTimer);
    }
}
