using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject houseLights;
    private bool isOn = false;

    public void ToggleLight()
    {
        isOn = !isOn;
        houseLights.SetActive(isOn);
        Debug.Log("LIGHT SWITCH: " + (isOn ? "ON" : "OFF"));
    }
}