using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [SerializeField] private float scrollSpeedX = 0.1f;
    [SerializeField] private float scrollSpeedY = 0.1f;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
