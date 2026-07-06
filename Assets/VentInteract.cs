using UnityEngine;

public class VentInteract : Interactable
{
    public GameObject ventCover;
    public GameObject itemInside;
    bool opened = false;

    void Start()
    {
        prompt = "Open Vent";
        itemInside.SetActive(false);
    }

    public override void Interact()
    {
        if (opened) return;

        if (!Inventory.instance.HasItem("Screwdriver"))
        {
            InteractionUI.Instance.Show("Need Screwdriver");
            return;
        }

        opened = true;
        ventCover.SetActive(false);
        itemInside.SetActive(true);
        InteractionUI.Instance.Hide();
    }
}
