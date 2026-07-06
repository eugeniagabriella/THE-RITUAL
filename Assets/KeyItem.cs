using UnityEngine;

public class KeyItem : Interactable
{
    public string keyID;

    public override void Interact()
    {
        Inventory.instance.AddItem(keyID);
        Debug.Log("Picked up key: " + keyID);
        Destroy(gameObject);
    }
}