using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhase5 : Phase
{
    [SerializeField] private float reviveTo;

    public override IEnumerator beginPhase()
    {
        owner.setHitPoints(reviveTo);
        owner.setInvulnerable(true);
        owner.gameObject.transform.position = new Vector3(0, 0, 0);
        Debug.Log("EPIC SMOKE PARTICLES! FROM THE ASHES I RISE!!!!!");
        yield return new WaitForSeconds(3.0f);
        owner.setInvulnerable(false);
        // Animations would go here
        yield return delayAction();
    }

    public override IEnumerator delayAction()
    {
        // choose movement
        yield return new WaitForSeconds(0.1f);
        yield return attackAction();
    }

    public override IEnumerator attackAction()
    {
        owner.cast("Tutorial Delayed Strike");
        owner.cast("Tutorial Laser Attack");
        owner.cast("Tutorial 5 Shotgun");
        yield return new WaitForSeconds(2.2f);
        yield return delayAction();
    }
}
