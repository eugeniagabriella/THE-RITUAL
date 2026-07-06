using UnityEngine;

public class DoorLocked : Interactable
{
    public string requiredKeyID = "RoomDoor";
    public bool isLocked = true;

    public Animator doorAnimator;
    public Collider doorCollider; // penting biar bisa dilewatin

    public override void Interact()
    {
        if (isLocked)
        {
            if (Inventory.instance != null && Inventory.instance.HasItem(requiredKeyID))
            {
                UnlockDoor();
            }
            else
            {
                Debug.Log("You need the correct key!");
                return;
            }
        }

        OpenDoor();
    }

    void UnlockDoor()
    {
        isLocked = false;

        // hapus key dari tangan player
        Inventory.instance.RemoveItem();

        // matikan collider supaya bisa dilewati
        if (doorCollider != null)
            doorCollider.enabled = false;

        Debug.Log("Door unlocked!");
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
    }
}