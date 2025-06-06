using UnityEngine;

public class OutlineHandler : MonoBehaviour
{
    public Material outlineMaterial;

    private Renderer objectRenderer;
    private Material[] originalMaterials;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterials = objectRenderer.materials;
    }

    public void EnableOutline()
    {
        Material[] newMats = new Material[originalMaterials.Length + 1];
        originalMaterials.CopyTo(newMats, 0);
        newMats[newMats.Length - 1] = outlineMaterial;
        objectRenderer.materials = newMats;
    }

    public void DisableOutline()
    {
        objectRenderer.materials = originalMaterials;
    }
}
