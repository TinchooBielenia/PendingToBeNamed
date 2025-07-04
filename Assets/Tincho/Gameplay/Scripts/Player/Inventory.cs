using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] private List<string> items = new List<string>();
    [SerializeField] private GameObject _slot1;
    //[SerializeField] private GameObject _slot2;
    //[SerializeField] private GameObject _slot3;

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

    private void Start()
    {
        _slot1.SetActive(false);
        //_slot2.SetActive(false);
        //_slot3.SetActive(false);
    }

    public void AddPickedItemToInventory(string objectPicked)
    {
        items.Add(objectPicked);
        Debug.Log("Item agregado al inventario: " + objectPicked);
        _slot1.SetActive(true);
    }

    public void DeletePickedItemFromInventory(string objectPicked)
    {
        items.Remove(objectPicked);
        Debug.Log("Item eliminado del inventario: " + objectPicked);
        _slot1.SetActive(false);
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}
