using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHidden = false;
    public Transform currentHidePoint;
    public MonsterAI monster;

    CharacterController controller;
    Renderer[] renderers;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (currentHidePoint != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHidden)
                Hide();
            else
                Unhide();
        }
    }

    void Hide()
    {
        isHidden = true;

        controller.enabled = false;
        transform.position = currentHidePoint.position;

        foreach (Renderer r in renderers)
            r.enabled = false;

        monster?.SetPlayerHidden(true);
    }

    void Unhide()
    {
        isHidden = false;

        foreach (Renderer r in renderers)
            r.enabled = true;

        controller.enabled = true;

        monster?.SetPlayerHidden(false);
    }

    public void SetHidePoint(Transform point)
    {
        currentHidePoint = point;
    }

    public void ClearHidePoint()
    {
        currentHidePoint = null;
    }
}