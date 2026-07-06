using UnityEngine;

public class DoorPassword : MonoBehaviour
{
    public bool isUnlocked = false;
    public Transform doorPivot;
    public float openAngle = 90f;
    bool isOpen = false;

    public void Unlock()
    {
        isUnlocked = true;
        UIManager.instance.ShowMessage("Door unlocked");
    }

    public void TryOpen()
    {
        if (!isUnlocked)
        {
            UIManager.instance.ShowMessage("Door needs a code");
            return;
        }

        if (isOpen) return;

        isOpen = true;
        doorPivot.Rotate(Vector3.up * openAngle);
        UIManager.instance.ShowMessage("Door opened");
    }
}