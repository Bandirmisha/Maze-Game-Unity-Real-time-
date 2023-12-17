using MazeGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player => GameManager.instance.player;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 11, -2);
    }
}
