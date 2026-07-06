using UnityEngine;
using System.Collections;

public class DoorInteract : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isLocked = false;
    public bool requireSouls = false;

    public Vector3 openOffset = new Vector3(0f, 0f, -0.7f);
    public float openSpeed = 2f;

    Vector3 closedPos;
    Vector3 openPos;

    bool isOpen = false;
    bool isMoving = false;

    Collider doorCollider;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;

        doorCollider = GetComponent<Collider>();

        Debug.Log("DOOR READY");
    }

    // DIPANGGIL PAS TEKAN E
    public void TryOpen()
    {
        Debug.Log("TRY OPEN");

        if (isMoving) return;

        // CEK SOUL
        if (requireSouls && !GameProgress.instance.HasAllSouls())
        {
            UIManager.instance?.ShowMessage("Something is holding the door...");
            return;
        }

        // CEK LOCK (CUMA PAS MAU BUKA)
        if (!isOpen && isLocked)
        {
            UIManager.instance?.ShowMessage("Door is locked");
            return;
        }

        StopAllCoroutines();
        StartCoroutine(MoveDoor(isOpen ? closedPos : openPos));
    }

    IEnumerator MoveDoor(Vector3 targetPos)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                Time.deltaTime * openSpeed
            );
            yield return null;
        }

        transform.position = targetPos;
        isOpen = !isOpen;

        // COLLIDER:
        // buka = mati, tutup = nyala
        if (doorCollider != null)
            doorCollider.enabled = !isOpen;

        isMoving = false;

        Debug.Log(isOpen ? "DOOR OPEN" : "DOOR CLOSED");
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("DOOR UNLOCKED");
    }
}