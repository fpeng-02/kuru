using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float speed;
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashTimeLength;
    private bool dashing;
    private float dashTimer;
    public Rigidbody2D rb;
    private float h;
    private float v;
    private Vector3 dirVect;
    [SerializeField] private Camera worldCam;

    void Start()
    {
        worldCam = GameObject.Find("Main Camera").GetComponent<Camera>(); // maybe wonky if we rename the main camera but proobably no need to
        dashing = false;
    }

    public void Update()
    {
        // get dash
        if (!dashing && Input.GetButtonDown("Dash")) {
            // TODO: dash animation?
            dashTimer = 0.0f;
            dashing = true;
            Vector3 tmpMousePos = Input.mousePosition;
            tmpMousePos.z = 0;
            Vector3 tmpDir = worldCam.ScreenToWorldPoint(tmpMousePos) - this.transform.position;
            tmpDir.z = 0;
            dirVect = tmpDir.normalized;
        }

        if (Input.GetButtonDown("BasicAttack")) { cast(0); }
        if (Input.GetButtonDown("Special1")) { cast(1); }

        // if dashing, update dash timer
        if (dashing) {
            // TODO: ignore damage while dashing
            dashTimer += Time.deltaTime;
            if (dashTimer > dashTimeLength) {
                dashing = false;
            }
        }

        // basic horiz/vert movement
        if (!dashing) {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            dirVect = (new Vector3(h, v, 0)).normalized;
        }
    }
    public void FixedUpdate()
    {
        // may need to adjust speed to other cases like slowness effects
        // are dashes slower with a slow effect? will see later
        if (dashing) {
            speed = dashSpeed;
        } else {
            speed = moveSpeed;
        }
        move();
    }
    public override void move()
    {
        rb.MovePosition(rb.transform.position + dirVect * speed * Time.deltaTime);
    }
}
