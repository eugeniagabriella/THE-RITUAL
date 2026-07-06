using UnityEngine;

public class DownstairsTriggerUI : MonoBehaviour
{
    bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (UIManager.instance != null)
            {
                UIManager.instance.ShowMessage("The house is too dark.");
                UIManager.instance.ShowMessage("I need to fix the light.", 3f);
            }
        }
    }
}