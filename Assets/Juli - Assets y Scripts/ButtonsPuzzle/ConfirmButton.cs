using UnityEngine;

public class ConfirmButton : MonoBehaviour, IInteraction
{
    public ButtonsManager buttonsManager;

    public void TriggerInteraction()
    {
        if (buttonsManager != null)
        {
            buttonsManager.CheckPassword();
        }
    }
}
