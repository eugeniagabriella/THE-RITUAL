using UnityEngine;

public class SoulItem : MonoBehaviour
{
    public void TakeSoul()
    {
        Debug.Log("SOUL TAKEN");
        GameProgress.instance.AddSoul();
        Destroy(gameObject);
    }
}