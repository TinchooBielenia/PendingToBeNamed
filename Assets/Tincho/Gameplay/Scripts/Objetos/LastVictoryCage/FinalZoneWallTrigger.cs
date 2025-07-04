using Unity.VisualScripting;
using UnityEngine;

public class FinalZoneWallTrigger : MonoBehaviour
{
    [SerializeField] private CageMetallicDoor _cageDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cageDoor.FinalZoneBattle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.SetActive(false);
    }
}
