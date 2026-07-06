using UnityEngine;

public class PickUpItem : Interactable
{
    public string itemID;

    public override void Interact()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("Inventory is NULL!");
            return;
        }

        Debug.Log("Trying to pick up: " + itemID);
        Debug.Log("Currently holding: " + Inventory.instance.currentItem);

        if (Inventory.instance.IsHoldingSomething())
        {
            Debug.Log("HANDS FULL");
            return;
        }

        Inventory.instance.AddItem(itemID);
        Debug.Log("ITEM ADDED");
        Destroy(gameObject);
    }
}