using UnityEngine;

public class MetallicDoorKey : MonoBehaviour, IInteraction
{
    private bool _isInteracting;
    [SerializeField] private string _key;

    private void Start()
    {
        _isInteracting = false;
        _key = "Generator Key";
    }

    private void Update()
    {
        if (_isInteracting)
        {
            TakeKey();
        }
    }

    public void TriggerInteraction()
    {
        _isInteracting = true;
    }

    private void TakeKey()
    {
        PlayerItemsPickedUp.instance.AddPickedItemToInventory(_key);
        Destroy(gameObject);
    }
}
