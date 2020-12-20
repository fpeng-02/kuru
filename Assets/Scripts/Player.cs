using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float speed = 2;
    public Rigidbody2D rb;
    private float h;
    private float v;
    void Start()
    {
    }

    public void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }
    public void FixedUpdate()
    {
        move();
    }
    public override void move()
    {

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed;
        rb.MovePosition(rb.transform.position + tempVect);
    }
}
