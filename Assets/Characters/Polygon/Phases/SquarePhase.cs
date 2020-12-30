using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePhase : Phase
{
    [SerializeField] private Sprite[] diceSprites = new Sprite[6];
    [SerializeField] private int bounces;
    [SerializeField] private float speed;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private int curBounces;
    private bool rolling;
    private int rollNumber;

    public override IEnumerator beginPhase()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            // MOVEMENT
            // move in a straight line
            // when hit wall, reflect off and bonk on walls (physics mat should handle this)
            curBounces = 0;
            rb2d.drag = 0;
            rb2d.angularDrag = 0;
            rolling = true;
            rb2d.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
            yield return new WaitUntil(() => rolling == false);

            // ATTACK
            // choose what to spawn based on roll number
            // stay still? idk lol
            yield return new WaitForSeconds(2.0f);
        }
    }

    void OnCollisionEnter2D()
    {
        rollNumber = Random.Range(0, 6);
        spriteRenderer.sprite = diceSprites[rollNumber];
        curBounces++;
        if (curBounces == bounces) {
            rb2d.drag = 5;
            rb2d.angularDrag = 5;
            rolling = false;
        }
        Debug.Log("wall!");
    }

    void OnCollisionExit2D()
    {
        Debug.Log("exit!");
    }
}
