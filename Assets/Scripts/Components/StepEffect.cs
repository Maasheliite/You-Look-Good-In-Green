using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffect : MonoBehaviour
{
    public ParticleSystem stepPrefab;
    public float stepDistance = 15f;

    private void Start()
    {
        InvokeRepeating("Step", 0, stepDistance);
    }
    
    void Step()
    {
        StartCoroutine(StepCoroutine());
    }

    IEnumerator StepCoroutine()
    {
        var stepObject = Instantiate(stepPrefab, transform.position,Quaternion.identity);
        float rippleTime = 4f;
        float rippleIntensity = 0.001f;
        float timer = 0;

        float startTime = Time.time;


        while (timer < rippleTime)
        {
            timer += Time.deltaTime;
           /* stepLight.intensity = 1.5f+Mathf.Sin(timer);
            stepLight.spotAngle = Mathf.Lerp(1,maxAngle,timer/ rippleTime);*/
            yield return new WaitForSeconds(rippleIntensity);
        }
        GameObject.Destroy(stepObject.gameObject);
        yield break;
    }
}
