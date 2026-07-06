using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;
    public TextMeshProUGUI text;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string message)
    {
        text.text = message;
        text.gameObject.SetActive(true);
    }

    public void Hide()
    {
        text.gameObject.SetActive(false);
    }
}