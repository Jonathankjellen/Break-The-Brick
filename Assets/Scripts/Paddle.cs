using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody {get; private set;}
    public Vector2 direction {get; private set;}
    public float speed = 30f;
    public float maxBounceAngle = 75f;
    // Variable used for mouse movement
    private Vector3 offset;
    // Variable used for mouse movement
    private float z;
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero;
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            this.direction = Vector2.left;
        } else if(Input.GetKey(KeyCode.D)){
            this.direction = Vector2.right;
        } else {
            this.direction = Vector2.zero;
    }
    }
    private void FixedUpdate(){
        if(this.direction != Vector2.zero){
            this.rigidbody.AddForce(this.direction * this.speed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;
            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);
            
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }
    void OnMouseDown()
    {
        // Retrive the pressed object´s z-position
        z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Offset between object´s current position and the position of the mouse
        offset = gameObject.transform.position - 
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, z));
    }

    void OnMouseDrag()
    {
        // Sets the position of the object to where the mouse is
        transform.position = 
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, z)) 
        + offset;
    }
}
