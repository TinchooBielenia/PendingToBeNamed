using UnityEngine;

public class Interact : MonoBehaviour
{
    // This class handles how the player interacts with objects under the layer "Interactable" using Raycast and an Interface.

    [Header("Raycast Settings")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask _interactLayer;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private AudioSource _interactSFX;
    private PlayerStats _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerStats>();
    }

    private void Update()
    {
        Ray debugRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        Debug.DrawRay(debugRay.origin, debugRay.direction * interactDistance, Color.green);
        PlayerInteract();
    }

    // Player interacts with objects using Mouse 0.
    private void PlayerInteract()
    {
        if (InputController.Instance.InteractPressed)
        {

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance, _interactLayer))
            {
                _interactSFX.Play();
                IInteraction interactable = hit.collider.GetComponentInParent<IInteraction>();
                if (interactable != null)
                {
                    Debug.Log("Player is interacting with " + hit.collider.name);
                    interactable.TriggerInteraction();
                }
            }

          
        }


    }
}