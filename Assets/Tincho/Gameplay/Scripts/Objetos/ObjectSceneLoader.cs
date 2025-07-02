using UnityEngine;

//TP2 - Martin Bielenia
public class ObjectSceneLoader : MonoBehaviour, IInteraction
{
    [SerializeField] private string _desiredScene;

    public void TriggerInteraction()
    {
        SceneHanlder.Instance.TravelToTestRoom(_desiredScene);
    }
}
