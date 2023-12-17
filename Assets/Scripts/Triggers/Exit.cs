using MazeGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            GameManager.instance.player.GetComponent<Player>().Quit();
        }
    }
}
