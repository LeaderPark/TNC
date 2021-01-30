using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public GameObject player = null;
    private float move = 0.0f;

    void Update()
    {
        Vector2 dir = player.transform.position - this.transform.position;

        move = cameraSpeed * Time.deltaTime;
        Vector2 moveVector = new Vector2(dir.x * move, dir.y * move);

        this.transform.Translate(moveVector);
    }
}