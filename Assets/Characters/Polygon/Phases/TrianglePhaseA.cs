using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePhaseA : Phase
{
    [SerializeField] private GameObject bullet;
    private PolygonCollider2D polygonCollider;
    private Rigidbody2D rb2d;
    private float[] prismOffsets = { -30, -20, -10, 0, 10, 20, 30 };
    private Color[] prismColors = { Color.red, new Color(1f, 0.64f, 0f) , Color.yellow, Color.green, Color.blue, new Color(0.29f, 0f, 0.51f), Color.magenta };
    private int prismCounter = 0;
    [SerializeField] private Sprite triangleSprite;


    public override IEnumerator beginPhase()
    {
        GetComponent<SpriteRenderer>().sprite = triangleSprite;

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
                GameObject go = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, col.transform.eulerAngles.z + prismOffsets[prismCounter]));
                go.GetComponent<SpriteRenderer>().color = prismColors[prismCounter];
                Debug.Log("ouch");
                prismCounter++;
                if (prismCounter == 7) {
                    prismCounter = 0;
                }
            }
        }
    }
}
