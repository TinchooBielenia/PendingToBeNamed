using UnityEngine;

public abstract class BaseLoot : MonoBehaviour
{
    protected abstract void OnLootTrigger(GameObject player);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            OnLootTrigger(other.gameObject);
        }
    }
}
