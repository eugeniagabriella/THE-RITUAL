using UnityEngine;

public class InteractionRaycast : MonoBehaviour
{
    public float distance = 3f;
    Interactable current;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (current != interactable)
                {
                    current = interactable;
                    InteractionUI.Instance.Show("E - " + current.prompt);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    current.Interact();
                }

                return;
            }
        }

        current = null;
        InteractionUI.Instance.Hide();
    }
}