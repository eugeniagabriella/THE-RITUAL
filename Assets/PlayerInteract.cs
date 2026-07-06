using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E PRESSED");
            TryInteract();
        }
    }

    void TryInteract()
    {
        if (playerCamera == null)
        {
            Debug.LogError("PLAYER CAMERA NOT ASSIGNED!");
            return;
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 2f);

        if (!Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.Log("RAY MISSED");
            return;
        }

        Debug.Log("HIT OBJECT: " + hit.collider.name);

        Interactable interactable =
            hit.collider.GetComponent<Interactable>() ??
            hit.collider.GetComponentInParent<Interactable>();

        if (interactable != null)
        {
            Debug.Log("INTERACTABLE FOUND");
            interactable.Interact();
        }
        else
        {
            Debug.Log("NO INTERACTABLE SCRIPT ON OBJECT");
        }
    }
}