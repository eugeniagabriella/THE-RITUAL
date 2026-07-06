using UnityEngine;

public class BreakDoor : Interactable
{
    public GameObject blockCollider;

    public override void Interact()
    {
        if (!Inventory.instance.HasItem("MainDoorKey"))
        {
            Debug.Log("Need Key");
            return;
        }

        Debug.Log("Door unlocked!");

        if (blockCollider != null)
            blockCollider.SetActive(false);

        Inventory.instance.RemoveItem();
    }
}