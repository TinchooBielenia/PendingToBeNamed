using UnityEngine;

public class OutlineHandler : MonoBehaviour
{
    [SerializeField] private Material _outlineMaterial;

    private Renderer _objectRenderer;
    private Material[] _originalMaterials;

    void Awake()
    {
        _objectRenderer = GetComponent<Renderer>();
        _originalMaterials = _objectRenderer.materials;
    }

    public void EnableOutline()
    {
        Material[] newMats = new Material[_originalMaterials.Length + 1];
        _originalMaterials.CopyTo(newMats, 0);
        newMats[newMats.Length - 1] = _outlineMaterial;
        _objectRenderer.materials = newMats;
    }

    public void DisableOutline()
    {
        _objectRenderer.materials = _originalMaterials;
    }
}
