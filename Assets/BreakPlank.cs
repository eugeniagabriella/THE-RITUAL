using UnityEngine;

public class BreakPlank : Interactable
{
    public GameObject plankRoot;
    public GameObject blockCollider;

    public override void Interact()
    {
        if (!Inventory.instance.HasItem("Hammer"))
        {
            Debug.Log("Need Hammer");
            return;
        }

        Destroy(plankRoot);

        if (blockCollider != null)
            blockCollider.SetActive(false);

        Inventory.instance.RemoveItem();

        Debug.Log("Plank destroyed");
    }
}