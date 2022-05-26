using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody {get; private set;}
    public float speed = 500f;
    public bool isNew = true;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if(this.gameObject.name == "Ball")
        {
            ResetBall();
            isNew = false;
        } else {

        }
    }
    public void changeForce()
    {
        Vector2 force = Vector2.zero;
        force.x = 0;
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * speed);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * speed);
    }
    public void ResetBall()
    {
        this.transform.position = new Vector2(0f, -8);
        this.rigidbody.velocity = Vector2.zero;
        this.gameObject.SetActive(true);

        Invoke(nameof(SetRandomTrajectory), 1f);
    }
    public void setForceNewballs(Transform pos)
    {
        //this.transform.position = new Vector2(pos.position.x, pos.position.y);
        //this.transform.position = new Vector2(0, 0);
        this.rigidbody.velocity = Vector2.zero;

        Vector2 force = Vector2.zero;
        force.x = 0;
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * speed/2);
    }
    
}
