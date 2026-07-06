using UnityEngine;

public class RitualDoor : Interactable
{
    public bool isLocked = true;
    public Animator animator;

    public override void Interact()
    {
        if (isLocked)
        {
            Debug.Log("The door is locked...");
            return;
        }

        OpenDoor();
    }

    public void UnlockDoor()
    {
        isLocked = false;
        Debug.Log("Door unlocked!");
    }

    void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            // Kalau belum pakai animasi
            transform.Rotate(0, 90, 0);
        }
    }
}