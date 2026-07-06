using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress instance;

    public int soulsCollected = 0;
    public int soulsRequired = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddSoul()
    {
        soulsCollected++;
        Debug.Log("SOUL COLLECTED: " + soulsCollected + "/" + soulsRequired);
    }

    public bool HasAllSouls()
    {
        return soulsCollected >= soulsRequired;
    }
}