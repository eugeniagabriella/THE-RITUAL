using UnityEngine;

public class CollectionSpot : Interactable
{
    [System.Serializable]
    public class RitualRequirement
    {
        public string itemID;
        public Transform placePoint;
        public GameObject placedPrefab;
        [HideInInspector] public bool alreadyPlaced;
    }

    public RitualRequirement[] requirements;
    public RitualDoor ritualDoor; // drag door ke sini

    public override void Interact()
    {
        if (!Inventory.instance.IsHoldingSomething())
        {
            Debug.Log("You are not holding anything.");
            return;
        }

        string heldItem = Inventory.instance.currentItem;

        foreach (var req in requirements)
        {
            if (req.itemID == heldItem)
            {
                if (req.alreadyPlaced)
                {
                    Debug.Log("Item already placed.");
                    return;
                }

                Instantiate(req.placedPrefab,
                            req.placePoint.position,
                            req.placePoint.rotation);

                req.alreadyPlaced = true;
                Inventory.instance.RemoveItem();

                Debug.Log(heldItem + " placed.");

                CheckCompletion();
                return;
            }
        }

        Debug.Log("This item doesn't belong here.");
    }

    void CheckCompletion()
    {
        foreach (var req in requirements)
        {
            if (!req.alreadyPlaced)
                return;
        }

        Debug.Log("RITUAL COMPLETE!");

        if (ritualDoor != null)
            ritualDoor.UnlockDoor();
    }
}