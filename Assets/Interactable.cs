using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string prompt = "E";

    public virtual void Interact()
    {
        // Akan dioverride di child class
    }
}