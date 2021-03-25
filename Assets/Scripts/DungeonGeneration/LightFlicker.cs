using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float minOuterRadius;
    [SerializeField] private float maxOuterRadius;
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private Light2D light2D;
    private float randomSpeed;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        light2D.intensity = minIntensity + Mathf.PingPong(randomSpeed * Time.time, maxIntensity - minIntensity);
        light2D.pointLightOuterRadius = minOuterRadius + Mathf.PingPong(randomSpeed/3 * Time.time, maxOuterRadius - minOuterRadius);
        if(light2D.pointLightOuterRadius <= minOuterRadius)
        {
            randomSpeed = Random.Range(minSpeed, maxSpeed);
        }
    }
}
