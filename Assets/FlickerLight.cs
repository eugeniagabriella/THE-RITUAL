using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour
{
    public Light targetLight;
    public AudioSource flickerSound;

    public float minWait = 25f;
    public float maxWait = 70f;

    public int minFlickers = 3;
    public int maxFlickers = 7;

    public float flickerSpeed = 0.06f;

    void Start()
    {
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            int flickers = Random.Range(minFlickers, maxFlickers);

            for (int i = 0; i < flickers; i++)
            {
                if (flickerSound != null)
                    flickerSound.Play();
                targetLight.enabled = false;
                yield return new WaitForSeconds(flickerSpeed);

                targetLight.enabled = true;
                yield return new WaitForSeconds(flickerSpeed);
            }
        }
    }
}