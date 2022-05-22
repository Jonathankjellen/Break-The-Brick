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
