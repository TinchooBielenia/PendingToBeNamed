using UnityEngine;

public class RandomizarCables : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentCable = transform.GetChild(i).gameObject;
            GameObject otherCable = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;

            Vector2 newPositionCurrentCable = otherCable.transform.position;
            Vector2 newPositionOtherCable = currentCable.transform.position;

            currentCable.transform.position = newPositionCurrentCable;
            otherCable.transform.position = newPositionOtherCable;
        }
    }
}
