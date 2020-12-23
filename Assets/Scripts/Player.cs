using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float baseMoveSpeed = 4;
    private bool dashing;
    private Rigidbody2D rb;
    private float h;
    private float v;
    private Vector3 dirVect;

    void Start()
    {
        dashing = false;
        rb = GetComponent<Rigidbody2D>();
        setSpeed(baseMoveSpeed);
    }

    public float getBaseMoveSpeed() { return this.baseMoveSpeed; }
    public void setDashing(bool dashing) { this.dashing = dashing; }
    public void setDirVect(Vector3 dirVect) { this.dirVect = dirVect; }


    public override void Update()
    {
        entityUpdate();

        if (!dashing && Input.GetButtonDown("Dash")) {
            cast("Player Dash");
        }
        if (Input.GetButtonDown("BasicAttack")) { cast(0); }
        if (Input.GetButtonDown("Special1")) { cast(1); }

        // basic horiz/vert movement
        if (!dashing) {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            dirVect = (new Vector3(h, v, 0)).normalized;
        }
    }
    public void FixedUpdate()
    {
        rb.MovePosition(rb.transform.position + dirVect * speed * Time.deltaTime);
    }
}
