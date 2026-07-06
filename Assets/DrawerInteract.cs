using UnityEngine;
using System.Collections;

public class DrawerInteract : Interactable
{
    public Vector3 openOffset = new Vector3(0, 0, 0.4f);
    public float speed = 5f;

    Vector3 closedPos;
    Vector3 openPos;
    bool isOpen;

    bool isMoving;

    void Awake()
    {
        closedPos = transform.localPosition;
        openPos = closedPos + openOffset;
    }

    public override void Interact()
    {
        if (isMoving) return; // anti spam bug

        StartCoroutine(MoveDrawer());
    }

    IEnumerator MoveDrawer()
    {
        isMoving = true;

        isOpen = !isOpen;
        Vector3 target = isOpen ? openPos : closedPos;

        while (Vector3.Distance(transform.localPosition, target) > 0.001f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        transform.localPosition = target;

        isMoving = false;
    }
}