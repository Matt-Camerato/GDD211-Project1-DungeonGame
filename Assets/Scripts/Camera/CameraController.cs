using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        float x = Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * 4);
        float y = Mathf.Lerp(transform.position.y, player.position.y, Time.deltaTime * 4);
        transform.position = new Vector3(x, y, -10);
    }
}
