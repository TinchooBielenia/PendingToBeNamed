using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public string entranceTagInNewScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneTransitionManager.Instance.TransitionToScene(sceneToLoad, entranceTagInNewScene);
        }
    }
}
