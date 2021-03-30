using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float customShakeTime;
    [SerializeField] private float customShakePower;
    [SerializeField] private float customRotationMultiplier;

    public static ScreenShake screenShakeInstance;

    public float secondsLeft;

    private float shakeCooldown = 1;

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
            ShakeScreen(0.4f, 0.08f, 7);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ShakeScreen(0.3f, 0.02f, 6);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ShakeScreen(customShakeTime, customShakePower, customRotationMultiplier);
        }

        //this causes a shake check to occur every second which determines whether a screen shake will play (to represent dungeon collapsing)
        if(shakeCooldown > 0)
        {
            shakeCooldown -= Time.deltaTime;
        }
        else
        {
            shakeCooldown = 1; //reset cooldown float

            //this will cause a shake to occur 30% of the time at the beginning of the game, but after 10 minutes will increase to 60% of the time
            var percentChanceToNot = 40 + (secondsLeft / 20); //this number descreases from 70 to 40 to show the percent chance that a shake WONT occur
            if (Random.Range(0f, 100f) > percentChanceToNot) 
            {
                ShakeScreen(0.4f, (100f - percentChanceToNot) / 1000f, 7f); //trigger shake with custom settings based on time elapsed
            }
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
