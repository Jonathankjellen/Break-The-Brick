using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health {get; private set;}
    public SpriteRenderer spriteRenderer {get; private set;}
    public Sprite[] states;
    public bool unbreakable;
    public int points = 100;
    public GameObject originalBall;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if(!unbreakable)
        {
            this.health = this.states.Length;
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }
    }
    private void Hit()
    {
        if(this.unbreakable){
            return;
        }
        this.health--;
        if(this.health <= 0)
        {
            this.gameObject.SetActive(false);
            // Release a new ball with random function
            GameObject newBall = GameObject.Instantiate(originalBall);
            newBall.name = "Ball";
            newBall.GetComponent<Ball>().ResetBall();
            FindObjectOfType<GameController>().balls.Add(newBall.GetComponent<Ball>());
        } else 
        {
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }
        FindObjectOfType<GameController>().Hit(this);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
