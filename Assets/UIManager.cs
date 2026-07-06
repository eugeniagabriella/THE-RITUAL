using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI")]
    public TextMeshProUGUI messageText;

    [Header("Settings")]
    public float defaultDuration = 2.5f;

    Coroutine currentRoutine;

    void Awake()
    {
        // Singleton safe
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Safety check
        if (messageText != null)
            messageText.text = "";
        else
            Debug.LogError("UIManager: messageText NOT ASSIGNED!");
    }

    /// <summary>
    /// Show a message on screen for a duration
    /// </summary>
    public void ShowMessage(string message, float duration = -1f)
    {
        if (messageText == null) return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        float time = duration > 0 ? duration : defaultDuration;
        currentRoutine = StartCoroutine(ShowRoutine(message, time));
    }

    IEnumerator ShowRoutine(string message, float duration)
    {
        messageText.text = message;
        yield return new WaitForSeconds(duration);
        messageText.text = "";
        currentRoutine = null;
    }
}