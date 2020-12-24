using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhase4 : Phase
{
    public override IEnumerator beginPhase()
    {
        // Animations would go here
        yield return delayAction();
    }

    public override IEnumerator delayAction()
    {
        // choose movement
        yield return new WaitForSeconds(1.0f);
        yield return attackAction();
    }

    public override IEnumerator attackAction()
    {
        owner.cast("Tutorial Delayed Strike");
        owner.cast("Tutorial Laser Attack");
        yield return new WaitForSeconds(2.2f);
        yield return delayAction();
    }
}
