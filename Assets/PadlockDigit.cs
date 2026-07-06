using UnityEngine;
using TMPro;

public class PadlockDigit : MonoBehaviour
{
    public int currentNumber = 0;
    public TextMeshProUGUI digitText;

    void Start()
    {
        UpdateText();
    }

    public void NextNumber()
    {
        currentNumber++;
        if (currentNumber > 9)
            currentNumber = 0;

        UpdateText();
    }

    void UpdateText()
    {
        digitText.text = currentNumber.ToString();
    }
}