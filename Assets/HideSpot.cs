using UnityEngine;

public class HideSpot : MonoBehaviour
{
    public Transform hidePoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHide>().SetHidePoint(hidePoint);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHide>().ClearHidePoint();
        }
    }
}