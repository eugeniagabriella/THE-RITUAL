using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public string currentItem = "";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasItem(string item)
    {
        return currentItem == item;
    }

    public bool IsHoldingSomething()
    {
        return !string.IsNullOrEmpty(currentItem);
    }

    public void AddItem(string item)
    {
        if (IsHoldingSomething())
        {
            Debug.Log("Already holding something!");
            return;
        }

        currentItem = item;
        Debug.Log("Picked up: " + item);
    }

    public void RemoveItem()
    {
        Debug.Log("Removed item: " + currentItem);
        currentItem = "";
    }
}