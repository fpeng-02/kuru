using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonPhase : Phase
{
    [SerializeField] private float interval;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Rigidbody2D rb2d;

    public override IEnumerator beginPhase()
    {
        rb2d = GetComponent<Rigidbody2D>();
        this.transform.position = Vector3.zero;
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            // small dash
            Quaternion randDir = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            rb2d.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * dashSpeed;
            yield return new WaitForSeconds(0.2f);
            rb2d.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.2f);

            // then spawn bees
            owner.cast("Create Bees");
            yield return new WaitForSeconds(interval);
        }
    }
}
