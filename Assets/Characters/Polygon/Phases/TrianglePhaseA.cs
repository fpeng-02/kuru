using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePhaseA : Phase
{
    [SerializeField] private GameObject bullet;
    private PolygonCollider2D polygonCollider;
    private Rigidbody2D rb2d;

    public override IEnumerator beginPhase()
    {
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;
        polygonCollider.enabled = true;
        this.transform.position = Vector3.zero;
        //Sprite to triangle
        owner.cast("Tri Laser");
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 10;
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        yield return null;
    }

    public override void exitPhase()
    {
        base.exitPhase();
        rb2d.angularVelocity = 0;
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        polygonCollider.enabled = false;
        polygonCollider.isTrigger = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (getPhaseActive()) {
            if (col.transform.gameObject.layer != LayerMask.NameToLayer("Boss")) {
                GameObject go = Instantiate(bullet, this.transform.position, col.transform.rotation);
                Debug.Log("ouch");
            }
        }
    }
}
