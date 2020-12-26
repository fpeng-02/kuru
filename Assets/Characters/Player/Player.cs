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

    [SerializeField] private float lavaTickInterval;
    [SerializeField] private float lavaTickDamage;

    void Start()
    {
        dashing = false;
        rb = GetComponent<Rigidbody2D>();
        setSpeed(baseMoveSpeed);
        StartCoroutine("tickLavaDamage");
    }

    public float getBaseMoveSpeed() { return this.baseMoveSpeed; }
    public void setDashing(bool dashing) { this.dashing = dashing; }
    public void setDirVect(Vector3 dirVect) { this.dirVect = dirVect; }

    public IEnumerator tickLavaDamage()
    {
        while (this.gameObject.activeSelf) {
            bool standingOnLava = false;
            foreach (Collider2D col in Physics2D.OverlapBoxAll(this.transform.position, new Vector2(0.5f, 0.5f), 0, LayerMask.GetMask("FloorTile"))) {
                if (col.gameObject.GetComponent<FloorTile>().getState() == FloorState.Lava) {
                    standingOnLava = true;
                }
            }
            if (!getInvulnerable() && standingOnLava) {
                hitPoints -= lavaTickDamage;
            }
            yield return new WaitForSeconds(lavaTickInterval);
        }
    }

    public override void customUpdate()
    {
        if (!dashing && Input.GetButtonDown("Dash")) {
            cast("Player Dash");
        }

        if (Input.GetButtonDown("BasicAttack")) { cast(0); }

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
