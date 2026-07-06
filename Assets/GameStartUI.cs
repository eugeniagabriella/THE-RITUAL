using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    void Start()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowMessage("Where am I?");
        }
    }
}