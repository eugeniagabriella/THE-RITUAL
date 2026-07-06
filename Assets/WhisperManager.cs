using UnityEngine;
using System.Collections;

public class WhisperManager : MonoBehaviour
{
    public AudioClip[] whispers;
    public AudioSource audioSource;

    public float minDelay = 30f;
    public float maxDelay = 90f;

    void Start()
    {
        StartCoroutine(WhisperRoutine());
    }

    IEnumerator WhisperRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(minDelay, maxDelay));

            if (whispers.Length > 0)
            {
                audioSource.clip =
                    whispers[Random.Range(0, whispers.Length)];

                audioSource.Play();
            }
        }
    }
}