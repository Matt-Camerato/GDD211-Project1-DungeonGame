using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownText : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float secondsLeft = 600;
    [SerializeField] private float rateOfTime;

    private TMP_Text countdownText;

    private void Start()
    {
        countdownText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (!player.won)
        {
            if (secondsLeft > 0)
            {
                ScreenShake.screenShakeInstance.secondsLeft = secondsLeft; //send seconds left to singleton instance of screenshake for use in collapsing screen shake effect

                var m = Mathf.Floor(Mathf.Ceil(secondsLeft) / 60); //minutes left
                var s = Mathf.Ceil(secondsLeft) % 60; //seconds left

                string sText = s.ToString(); //text for how many seconds are left (0 to 59)
                if (s < 10) { sText = "0" + sText; } //if seconds left is in single digits, add a "0" character before it ("7" will look like "07" and "0" will look like "00")

                countdownText.text = m.ToString() + ":" + sText; //update countdown display text

                secondsLeft -= Time.deltaTime * rateOfTime; //decrease seconds left if not below zero yet
            }
            else
            {
                //TIME HAS RUN OUT

                player.Damaged(100); //when time runs out, deal 100 damage to player, killing them instantly
            }
        }
    }
}
