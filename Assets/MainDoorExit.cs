using UnityEngine;

public class MainDoorExit : MonoBehaviour
{
    public GameObject winCanvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            winCanvas.SetActive(true);
        }
    }
}