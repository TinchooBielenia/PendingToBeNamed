using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [SerializeField] private float _scrollSpeedX = 0.1f;
    [SerializeField] private float _scrollSpeedY = 0.1f;
    private Renderer _rend;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float offsetX = Time.time * _scrollSpeedX;
        float offsetY = Time.time * _scrollSpeedY;
        _rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
