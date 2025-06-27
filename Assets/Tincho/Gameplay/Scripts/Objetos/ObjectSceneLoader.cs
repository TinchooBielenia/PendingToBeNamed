using UnityEngine;

public class ObjectSceneLoader : MonoBehaviour, IInteraction
{
    [SerializeField] private string _desiredScene;

    public void TriggerInteraction()
    {
        SceneHanlder.Instance.TravelToTestRoom(_desiredScene);
    }
}
