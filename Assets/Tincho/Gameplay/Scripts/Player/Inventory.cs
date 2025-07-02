using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] private List<string> items = new List<string>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddPickedItemToInventory(string objectPicked)
    {
        items.Add(objectPicked);
        Debug.Log("Item agregado al inventario: " + objectPicked);
    }

    public void DeletePickedItemFromInventory(string objectPicked)
    {
        items.Remove(objectPicked);
        Debug.Log("Item eliminado del inventario: " + objectPicked);
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}
