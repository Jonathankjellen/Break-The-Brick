using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball" || collision.gameObject.name == "NewBall")
        {
            collision.gameObject.SetActive(false);
            FindObjectOfType<GameController>().Miss();
        }
    }
}
