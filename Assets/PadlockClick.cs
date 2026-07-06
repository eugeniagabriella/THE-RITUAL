using UnityEngine;

public class PadlockClick : MonoBehaviour
{
    public PadlockDigit padlock;

    void OnMouseDown()
    {
        padlock.NextNumber();
    }
}