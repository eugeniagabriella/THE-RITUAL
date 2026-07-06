using UnityEngine;

public class MonsterSpawnTrigger : MonoBehaviour
{
    public GameObject monster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster.SetActive(true);
            Destroy(gameObject);
        }
    }
}