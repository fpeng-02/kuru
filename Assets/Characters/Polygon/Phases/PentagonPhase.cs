using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonPhase : Phase
{
    [SerializeField] private float interval;
    [SerializeField] private Sprite pentagonSprite;

    public override IEnumerator beginPhase()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<SpriteRenderer>().sprite = pentagonSprite;
        // ignore collisions between the boss and the wall for this phases since the boss comes through the walls 
        // the boss doesn't move in this phase, so it won't like clip through a wall by walking or something
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Boss"), LayerMask.NameToLayer("Environment"), true);
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            owner.cast("Meteor Shower");
            yield return new WaitForSeconds(interval);
        }
    }

    public override void exitPhase()
    {
        base.exitPhase();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Boss"), LayerMask.NameToLayer("Environment"), false);
        owner.transform.position = new Vector3(0, 0, 0);
    }
}
