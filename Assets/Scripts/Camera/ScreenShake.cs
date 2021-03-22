using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake screenShakeInstance;

    private float shakeTime;
    private float shakeFadeTime;
    private float shakePower;
    private float shakeRotation;
    private float rotationMultiplier;

    private void Start()
    {
        screenShakeInstance = this;   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShakeScreen(0.5f, 0.1f, 7);
        }
    }

    private void LateUpdate()
    {
        //after update function where shake can be called, check if it has and if so, cause screen shake
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime; //start countdown of shake

            //get random x and y shake amounts
            float xShake = Random.Range(-1f, 1f) * shakePower;
            float yShake = Random.Range(-1f, 1f) * shakePower;

            //then move camera by selected shake amounts
            transform.position += new Vector3(xShake, yShake, 0);

            //decrease power and rotation over time so shake starts out big and then fades
            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    public void ShakeScreen(float shakeTime, float shakePower, float rotationMultiplier)
    {
        this.shakeTime = shakeTime;
        this.shakePower = shakePower;
        this.rotationMultiplier = rotationMultiplier;

        shakeFadeTime = shakePower / shakeTime;

        shakeRotation = shakePower * rotationMultiplier;
    }
}
