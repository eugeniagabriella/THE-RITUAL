using UnityEngine;
using System.Collections;

public class DoorInteract1 : Interactable
{
    public Vector3 openRotation = new Vector3(0, 90, 0);
    public float speed = 5f;

    Quaternion closedRot;
    Quaternion openRot;
    bool isOpen;
    bool isMoving;

    void Awake()
    {
        closedRot = transform.localRotation;
        openRot = Quaternion.Euler(openRotation);
    }

    public override void Interact()
    {
        if (isMoving) return;
        StartCoroutine(RotateDoor());
    }

    IEnumerator RotateDoor()
    {
        isMoving = true;
        isOpen = !isOpen;

        Quaternion target = isOpen ? openRot : closedRot;

        while (Quaternion.Angle(transform.localRotation, target) > 0.5f)
        {
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                target,
                Time.deltaTime * speed
            );

            yield return null;
        }

        transform.localRotation = target;
        isMoving = false;
    }
}