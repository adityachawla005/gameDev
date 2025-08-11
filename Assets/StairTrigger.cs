using UnityEngine;

public class StairTrigger : MonoBehaviour
{
    public GameObject[] lightsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject lightObj in lightsToDisable)
            {
                Light light = lightObj.GetComponent<Light>();
                if (light != null)
                {
                    // Option 1: Fade out slowly
                    StartCoroutine(FadeLight(light, 1f));
                    
                    // Option 2: Instantly turn off
                    // light.enabled = false;
                }
            }
        }
    }

    System.Collections.IEnumerator FadeLight(Light light, float duration)
    {
        float startIntensity = light.intensity;
        float time = 0;

        while (time < duration)
        {
            light.intensity = Mathf.Lerp(startIntensity, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        light.intensity = 0;
        light.enabled = false;
    }
}
